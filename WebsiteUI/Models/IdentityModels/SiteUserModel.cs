using System.ComponentModel.DataAnnotations;

namespace WebsiteUI.Models.IdentityModels
{
    public class SiteUserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
