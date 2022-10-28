using WebsiteUI.Models;

namespace WebsiteUI.ViewModels
{
	public class CreateProductViewModel
	{
		public ProductModel ProductModel { get; set; } = new ProductModel();
		public List<CategoryModel> CategoryModels { get; set; } = new List<CategoryModel>();
		public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();

		public void AddImageFile(IFormFile file)
        {
			ImageFiles.Add(file);
        }

		public void AddImageFiles(List<IFormFile> files)
        {
			ImageFiles.AddRange(files);
        }
	}
}
