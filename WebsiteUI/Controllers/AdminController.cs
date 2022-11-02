using Business.Abstract;
using Entities;
using Entities.UserEntities.SiteUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebsiteUI.Extensions;
using WebsiteUI.FunctionsLibrary;
using WebsiteUI.Models;
using WebsiteUI.Models.OtherClasses;
using WebsiteUI.Models.PartialViewModels;
using WebsiteUI.ViewModels;
using WebsiteUI.ViewModels.AdminViewModels;

namespace WebsiteUI.Controllers
{
    /*
     TODO
    - Need to implement a system that holds necessary data while deleting objects.
    - Need to implement a system that settles popular products.
     */

    public class AdminController : Controller
    {
        //Configuration Parameters
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IImageService _imageService;

        private RoleManager<IdentityRole> _roleManager;
        private UserManager<SiteUser> _userManager;

        private AssistantFunctions helperLib = new AssistantFunctions();
        public AdminController(IProductService productService, ICategoryService categoryService, IImageService imageService,
            UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _imageService = imageService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        //General Parameters
        private int pageSize = 2;
        private int PopularProductSelectionRange = 50;

        public IActionResult Index()
        {
            return View();
        }

        //Product Actions

        public async Task<IActionResult> ProductList()
        {
            var products = await _productService.GetAllAsync();
            ProductListViewModel listModel = new ProductListViewModel();

            foreach (var product in products)
            {
                var pModel = helperLib.CreateProductModel(product);
                pModel.ImageModels = helperLib.CreateImageModels(_productService.GetProductImages(pModel.Id));
                listModel.ProductModels.Add(pModel);
            }

            return View(listModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            CreateProductViewModel viewModel = new CreateProductViewModel();
            var categories = await _categoryService.GetAllAsync();

            foreach (var category in categories)
            {
                viewModel.CategoryModels.Add(helperLib.CreateCategoryModel(category));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel viewModel, int[] categoryIds)
        {
            List<Image> imagesToAdd = new List<Image>();

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            Product entity = helperLib.CreateProduct(viewModel.ProductModel);

            entity.SetStockState(viewModel.ProductModel.StockStateNumber);
            entity.IsApproved = true;

            if (viewModel.ImageFiles.Count > 0)
            {
                bool isFirst = true;

                foreach (var file in viewModel.ImageFiles)
                {
                    string extension = Path.GetExtension(file.FileName).ToLower();
                    string generatedName = string.Format($"{Guid.NewGuid()}{extension}");

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", generatedName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    if (isFirst)
                    {
                        var image = helperLib.CreateImage(new ImageModel
                        {
                            Url = generatedName,
                            IsMainImageOfProduct = true,
                            IsHomeDisplay = false
                        });
                        _imageService.Create(image);
                        imagesToAdd.Add(image);
                        entity.MainImageUrl = image.Url;
                    }
                    else
                    {
                        var image = helperLib.CreateImage(new ImageModel
                        {
                            Url = generatedName,
                            IsMainImageOfProduct = false,
                            IsHomeDisplay = false
                        });
                        _imageService.Create(image);
                        imagesToAdd.Add(image);
                    }
                }
            }


            if (_productService.Create(entity))
            {
                _productService.AddCategories(entity.Id, categoryIds);
                foreach (var image in imagesToAdd)
                {
                    _productService.AddImage(entity.Id, image.Id);
                }
                CreateMessage("yeni ürün eklendi.", "success", "Başarılı");
                return RedirectToAction("ProductList");
            }
            else
            {
                CreateMessage(_productService.ErrorMessage, "danger", "Ürün Oluştururken Hata");
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product entity = _productService.GetById(productId);

            if (entity == null)
            {
                return NotFound();
            }

            ProductModel model = helperLib.CreateProductModel(entity);
            model.ImageModels = helperLib.CreateImageModels(_productService.GetProductImages(productId));
            model.SelectedCategories = _productService.GetCategoriesOfProduct(entity.Id);
            ViewBag.Categories = await _categoryService.GetAllAsync();
            model.Id = productId;

            EditProductViewModel viewModel = new EditProductViewModel();
            viewModel.ProductModel = model;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel model, int[] categoryIds)
        {

            Product entity = _productService.GetById(model.ProductModel.Id);
            ProductModel pModel = model.ProductModel;
            List<Image> imagesToAdd = new List<Image>();

            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = pModel.Name;
            entity.ProductUrl = helperLib.GenerateUrl(entity.Name);
            entity.CardDescription = pModel.CardDescription;
            entity.Description = pModel.Description;
            //entity.BaseDolarPrice = pModel.BaseDolarPrice;
            entity.BasePrice = pModel.BasePrice;
            entity.SalePrice = pModel.SalePrice;
            entity.Quantity = pModel.Quantity;
            entity.IsApproved = pModel.IsApproved;
            entity.IsSuggested = pModel.IsHomeDisplay;
            entity.IsAltered = true;
            entity.SetStockState(pModel.StockStateNumber);
            entity.SetBasePriceType(pModel.BasePriceTypeNumber);
            if (model.ImageFiles.Count > 0)
            {
                bool isFirst = true;

                foreach (var file in model.ImageFiles)
                {
                    string extension = Path.GetExtension(file.FileName).ToLower();
                    string generatedName = string.Format($"{Guid.NewGuid()}{extension}");

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", generatedName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    if (isFirst)
                    {
                        var image = helperLib.CreateImage(new ImageModel
                        {
                            Url = generatedName,
                            IsMainImageOfProduct = true,
                            IsHomeDisplay = false
                        });
                        entity.MainImageUrl = image.Url;
                        _imageService.Create(image);
                        imagesToAdd.Add(image);
                    }
                    else
                    {
                        var image = helperLib.CreateImage(new ImageModel
                        {
                            Url = generatedName,
                            IsMainImageOfProduct = false,
                            IsHomeDisplay = false
                        });
                        _imageService.Create(image);
                        imagesToAdd.Add(image);
                    }
                }
            }

            if (_productService.Update(entity, categoryIds))
            {
                foreach (var image in imagesToAdd)
                {
                    _productService.AddImage(entity.Id, image.Id);
                }
                CreateMessage("düzenleme başarılı", "success", "Ürün Başarıyla Düzenlendi");
                return RedirectToAction("ProductList");
            }
            else
            {
                CreateMessage("error mesaj", "danger", "Ürün Düzenlerken Hata");
                return View(model);
            }
            return View(model);
        }

        public IActionResult RemoveImageFromProduct(int productId, string imageUrl)
        {//IMPLEMENT
            _imageService.RemoveImageFromProduct(productId, imageUrl);
            return Redirect("~/admin/editproduct/" + productId);
        }

        public IActionResult DeleteProduct(int productId)
        {
            Product entity = _productService.GetById(productId);

            if (entity != null)
            {
                _productService.Delete(entity);
            }

            AlertMessage tempMessage = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün silindi",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(tempMessage);

            return RedirectToAction("ProductList");
        }

        public IActionResult SettlePopularProducts()
        {
            int numberOfProducts = _productService.GetProductsCount();
            int overflow = numberOfProducts % 6;
            numberOfProducts -= overflow;

            if (PopularProductSelectionRange <= numberOfProducts) PopularProductSelectionRange *= 2;

            List<Product> newProducts = _productService.GetRandomPopularProducts(numberOfProducts, PopularProductSelectionRange);
            int[] newProductIds = new int[newProducts.Count];
            int i = 0;
            foreach (var product in newProducts)
            {
                newProductIds[i] = product.Id;
                i++;
            }

            _productService.AssignNewPopularProducts(newProductIds);

            return RedirectToAction("AdminPage");
        }

        //Category Actions

        public async Task<IActionResult> CategoryList()
        {
            var categories = await _categoryService.GetAllAsync();
            CategoryListModel listModel = new CategoryListModel();

            foreach (var category in categories)
            {
                listModel.categoryModels.Add(helperLib.CreateCategoryModel(category));
            }

            return View(listModel);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                Category entity = helperLib.CreateCategory(model);

                _categoryService.Create(entity);

                AlertMessage tempMessage = new AlertMessage()
                {
                    Message = $"{entity.Name} isimli kategori eklendi",
                    AlertType = "success"
                };

                TempData["message"] = JsonConvert.SerializeObject(tempMessage);
                return RedirectToAction("CategoryList");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int categoryId, int page = 1)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            EditCategoryViewModel viewModel = new EditCategoryViewModel();

            Category entity = await _categoryService.GetByIdAsync(categoryId);

            viewModel.ProductModels = helperLib.CreateProductModels(await _productService.GetProductsByCategoryAsync(entity.CategoryUrl, pageSize, page, true));

            viewModel.CategoryModel = helperLib.CreateCategoryModel(entity);
            viewModel.PageInfo = new PageInfo()
            {
                TotalItems = _productService.GetCountByCategory(entity.CategoryUrl),
                CurrentPage = page,
                ItemsPerPage = pageSize,
                CurrentCategoryUrl = entity.CategoryUrl
            };

            foreach (var model in viewModel.ProductModels)
            {
                model.ImageModels = helperLib.CreateImageModels(_productService.GetProductImages(model.Id));
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditCategory(EditCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Category entity = _categoryService.GetById(viewModel.CategoryModel.Id);

                if (entity == null)
                {
                    Console.WriteLine("entity == null");
                    Console.WriteLine("entity == null");
                    Console.WriteLine("entity == null");
                    return NotFound();
                }

                entity.Name = viewModel.CategoryModel.Name;
                entity.CategoryUrl = helperLib.GenerateUrl(viewModel.CategoryModel.Name);

                _categoryService.Update(entity);

                CreateMessage("Kategori düzenlenmesi tamamlandı.", "success", "İşlem Başarılı");

                return RedirectToAction("CategoryList");
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RemoveFromCategory(int productId, int categoryId)
        {
            _categoryService.RemoveFromCategory(productId, categoryId);
            return Redirect("~/admin/editcategory/" + categoryId);
        }

        public IActionResult DeleteCategory(int categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            Category entity = _categoryService.GetById(categoryId);

            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

            AlertMessage tempMessage = new AlertMessage()
            {
                Message = $"{entity.Name} isimli kategori silindi",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(tempMessage);

            return RedirectToAction("CategoryList");
        }

        public IActionResult AdminPage()
        {
            return View();
        }


        //Identity Actions

        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(viewModel.RoleModel.RoleName));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList", "Admin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RoleEdit(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            var members = new List<SiteUser>();
            var nonMembers = new List<SiteUser>();

            if (role == null)
            {
                Console.WriteLine("role is null with given Id");
                Console.WriteLine("given Id: " + roleId);
            }

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    members.Add(user);
                }
                else
                {
                    nonMembers.Add(user);
                }
            }

            RoleEditViewModel viewModel = new RoleEditViewModel()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers,
                NewRoleName = role.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditViewModel viewModel)
        {
            Console.WriteLine("in httppost roleedit");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(viewModel);
            }

            if (viewModel.IdsToDelete != null)
            {
                //removes users from role
                foreach (var userId in viewModel.IdsToDelete)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, viewModel.Role.Name);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }

            if (viewModel.IdsToAdd != null)
            {
                foreach (var userId in viewModel.IdsToAdd)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, viewModel.Role.Name);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(viewModel.NewRoleName))
            {
                var role = await _roleManager.FindByIdAsync(viewModel.Role.Id);
                role.Name = viewModel.NewRoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    CreateMessage("İsim başarılı bir şekilde" + viewModel.NewRoleName + "olarak değiştirildi.", "success", "İsim Değiştirme Başarılı");
                }
                else
                {
                    CreateMessage("İsim değiştirme başarısız.", "danger", "İsim Değiştirme Başarısız");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }

            return Redirect("/admin/roleedit/" + viewModel.Role.Id);
        }

        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);

                ViewBag.roleNames = GetRoleNames();
                return View(new UserEditViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles
                });
            }
            CreateMessage("No user with this Id", "warning", "Error");
            return Redirect("~/admin/userlist");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel viewModel, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.UserId);

                if (user != null)
                {
                    user.FirstName = viewModel.FirstName;
                    user.LastName = viewModel.LastName;
                    user.UserName = viewModel.UserName;
                    user.EmailConfirmed = viewModel.EmailConfirmed;
                    user.Email = viewModel.Email;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        if (selectedRoles == null)
                        {
                            selectedRoles = new string[] { };
                        }
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>()); //adds not already included roles.
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>()); //removes selected roles

                        ViewBag.roleNames = GetRoleNames();
                        return View(viewModel);
                    }
                }
                CreateMessage("Bu ID ile bir kullanıcı bulunamadı.", "danger", "Kullanıcı bulunamadı.");
                return Redirect("/admin/userlist");
            }
            CreateMessage("Değişiklikler işlemeye uygun değil.", "danger", "Model Hatası");

            ViewBag.roleNames = GetRoleNames();
            return View(viewModel);
        }

        //functions

        public List<string> GetRoleNames()
        {
            List<string> roleNames = new List<string>();
            foreach (var role in _roleManager.Roles)
            {
                roleNames.Add(role.Name);
            }

            return roleNames;
        }

        public void CreateMessage(string message, string alertType, string messageTitle)
        {
            TempData.Put("message", new AlertMessage()
            {
                Message = message,
                Title = messageTitle,
                AlertType = alertType
            });
        }

        public void Test()
        {
            Console.WriteLine("Test");
        }
    }
}
