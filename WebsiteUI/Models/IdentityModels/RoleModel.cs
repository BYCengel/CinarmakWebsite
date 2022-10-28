using System.ComponentModel.DataAnnotations;

namespace WebsiteUI.Models.IdentityModels
{
    public class RoleModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
