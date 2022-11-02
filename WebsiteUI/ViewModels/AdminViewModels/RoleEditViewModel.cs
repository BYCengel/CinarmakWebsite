using Entities.UserEntities.SiteUser;
using Microsoft.AspNetCore.Identity;

namespace WebsiteUI.ViewModels.AdminViewModels
{
    public class RoleEditViewModel
    {
        public IdentityRole Role { get; set; }
        public List<SiteUser> Members { get; set; } = new List<SiteUser>();
        public List<SiteUser> NonMembers { get; set; } = new List<SiteUser>();
        public string[]? IdsToAdd { get; set; }
        public string[]? IdsToDelete { get; set; }
        public string NewRoleName { get; set; } = "";
    }
}
