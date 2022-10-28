using Entities.UserEntities.SiteUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebsiteUI.Extensions;
using WebsiteUI.FunctionsLibrary;
using WebsiteUI.FunctionsLibrary.EmailServices;
using WebsiteUI.Models.OtherClasses;
using WebsiteUI.ViewModels.AccountViewModels;

namespace WebsiteUI.Controllers
{
    /* TODO LIST
     - send another verification mail in case of not confirmed mail
     */
    public class AccountController : Controller
    {
        private UserManager<SiteUser> _userManager;
        private SignInManager<SiteUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private IEmailSender _emailSender;
        private AssistantFunctions helperLib = new AssistantFunctions();

        public AccountController(UserManager<SiteUser> userManager, SignInManager<SiteUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {//injection
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {//gonna need to create a function for SiteUser creation. Will take care of this then.
            Console.WriteLine("in login action");
            if (await _userManager.FindByNameAsync("admin") == null)
            {
                Console.WriteLine("userfindbyname(admin) == null");
                if (await _roleManager.FindByNameAsync("Admin") == null)
                {
                    Console.WriteLine("rolefindbyname(admin) == null");
                    await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                }
                SiteUser user = new SiteUser()
                {
                    Email = "admin@hebere.com",
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "admin",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, "Ell45alb*");
                await _userManager.AddToRoleAsync(user, "Admin");
                Console.WriteLine("created initial admin.");
            }

            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByNameAsync(viewModel.UserModel.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "böyle bir kullanıcı bulunmamaktadır. Lütfen giriş bilgilerinizi kontrol ediniz.");
                return View(viewModel);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                CreateMessage(message: "Üyeliğinizi onaylama işlemleri tamamlanmamış. Lütfen mail adresinize gelen link ile üyeliğinizi onaylayın.", "danger",
                    "Onaylama İşlemi Başarısız");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user, viewModel.UserModel.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                if (viewModel.ReturnUrl != null)
                {
                    return Redirect(viewModel.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Giriş işlemi başarısız.");
                return View(viewModel);
            }
        }

        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            CreateMessage("Oturum kapatıldı.", "warning", "Logout");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            SiteUser user = new SiteUser()
            {
                Email = viewModel.RegisterModel.Email,
                FirstName = viewModel.RegisterModel.FirstName,
                LastName = viewModel.RegisterModel.LastName,
                UserName = viewModel.RegisterModel.UserName
            };

            var result = await _userManager.CreateAsync(user, viewModel.RegisterModel.Password);
            //Console.WriteLine(result.Succeeded + result.Errors.FirstOrDefault().Description + " " + viewModel.RegisterModel.Password);
            if (result.Succeeded)
            {
                if (await _roleManager.FindByNameAsync("Customer") == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole() { Name = "Customer" });
                }
                await _userManager.AddToRoleAsync(user, "Customer");

                //generate token
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string tokenUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = token
                });
                //send the token with email
                var emailBody = $"Lütfen hesabınızı onaylamak için <a href='https://localhost:7175{tokenUrl}'>Bu linke tıklayınız.<a>";
                _emailSender.SendEmail(new EmailDTO()
                {
                    To = viewModel.RegisterModel.Email,
                    Subject = "Çınarmak Hesap Onay Maili",
                    Body = emailBody
                });
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Bilinmeyen bir hata oldu. Lütfen yeniden deneyiniz.");
            return View(viewModel);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId == null || token == null)
            {
                //TODO => might send another confirmation mail here.
                CreateMessage("geçersiz token.", "danger", "Geçersiz Token.");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);

            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    //TODO: instantiate cart object
                    CreateMessage("hesabınız başarılı bir şekilde onaylandı. Diğer işlemlerinize devam edebilirsiniz.", "success", "Hesap Onaylama Başarılı");
                    return View();
                }
            }
            CreateMessage("hesabınız onaylanmadı.", "danger", "Hesap Onaylanmadı");

            return View();
        }


        //functions
        public void CreateMessage(string message, string alertType, string messageTitle)
        {
            TempData.Put("message", new AlertMessage()
            {
                Message = message,
                Title = messageTitle,
                AlertType = alertType
            });
        }
    }
}
