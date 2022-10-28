using WebsiteUI.Models.IdentityModels;

namespace WebsiteUI.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        public SiteUserModel UserModel { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
