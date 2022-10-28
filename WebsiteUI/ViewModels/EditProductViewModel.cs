using WebsiteUI.Models;

namespace WebsiteUI.ViewModels
{
    public class EditProductViewModel
    {
        public ProductModel ProductModel { get; set; }
        public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
	}
}
