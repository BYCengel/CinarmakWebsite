using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebsiteUI.FunctionsLibrary;
using WebsiteUI.Models;

namespace WebsiteUI.Controllers
{
    /*
     TODO
    - A system to take care of stock state situations. DON'T CHANGE STOCK STATE IN CASE OF ON ORDER
    - product list listeleme işlemleri
     */

    public class ProductController : Controller
    {

        private IProductService _productService;
        private ICategoryService _categoryService;
        private AssistantFunctions helperLib = new AssistantFunctions();

        private int pageSize = 24;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(string categoryUrl, int page = 1, int sortingIndex = 4)
        {
            ProductListViewModel listModel = new ProductListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetCountByCategory(categoryUrl),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentCategoryUrl = categoryUrl
                },
                CurrentAction = "List"
            };
            var products = await _productService.GetProductsByCategoryAsync(categoryUrl, pageSize, page, false);
            var categories = await _categoryService.GetAllAsync();

            foreach (Product product in products)
            {
                ProductModel productModel = helperLib.CreateProductModel(product);
                productModel.ImageModels.AddRange(helperLib.CreateImageModels(_productService.GetProductImages(productModel.Id)));

                listModel.ProductModels.Add(productModel);
            }

            if (!string.IsNullOrEmpty(categoryUrl))
            {
                _categoryService.IncreaseCategoryViewCount(categoryUrl);
            }
            
            if(sortingIndex != -1)
            {
                listModel.ProductModels = helperLib.OrderProductModels(listModel.ProductModels, sortingIndex);
            }

            return View(listModel);
        }

        public IActionResult Details(string productUrl)
        {
            string refactoredProductUrl = helperLib.GenerateUrl(productUrl);

            if (refactoredProductUrl == null)
            {
                Console.WriteLine("NOT FOUND NOT FOUND NOT FOUND");
                return NotFound();
            }

            Product product = _productService.GetProductDetails(refactoredProductUrl);

            if (product == null)
            {
                Console.WriteLine("PRODUCT == NULL");
                return NotFound();
            }

            ProductModel model = helperLib.CreateProductModel(product);

            model.SelectedCategories = product.ProductCategories.Select(pc => pc.Category).ToList();
            var images = product.ProductImages.Select(pi => pi.Image).ToList();

            foreach (var image in images)
            {
                model.ImageModels.Add(helperLib.CreateImageModel(image));
            }

            _productService.IncreaseProductViewCount(refactoredProductUrl);

            return View(model);
        }

        public async Task<IActionResult> Search(string searchString, int sortingIndex = -1)
        {
            ProductListViewModel listModel = new ProductListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    CurrentPage = 1,
                    ItemsPerPage = pageSize
                },
                CurrentAction = "Search",
                SearchString = searchString
            };
            var products = await _productService.GetSearchResultsAsync(searchString);
            listModel.PageInfo.TotalItems = products.Count;
            foreach (var product in products)
            {
                listModel.ProductModels.Add(helperLib.CreateProductModel(product));
            }

            if (sortingIndex != -1)
            {
                listModel.ProductModels = helperLib.OrderProductModels(listModel.ProductModels, sortingIndex);
            }

            return View("List", listModel);
        }
    }
}


// Sort action prototype

/*public async Task<IActionResult> Sort(int sortingIndex, ProductListViewModel viewModel)
        {
            Console.WriteLine("In Sort Action");

            ProductListViewModel listModel = new ProductListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    CurrentPage = 1,
                    ItemsPerPage = pageSize
                }
            };

            if(viewModel.ProductModels.Count > 0)
            {
                var orderedProducts = helperLib.OrderProductModels(viewModel.ProductModels, sortingIndex);
                listModel.ProductModels.AddRange(orderedProducts);
            }
            else
            {
                var products = await _productService.GetSortedProducts(sortingIndex);

                foreach (var product in products)
                {
                    listModel.ProductModels.Add(helperLib.CreateProductModel(product));
                }
            }

            

            return View("List", listModel);
        }*/
