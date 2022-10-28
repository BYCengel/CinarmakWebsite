using WebsiteUI.Models;

namespace WebsiteUI.ViewModels
{
    public class EditCategoryViewModel
    {
        public CategoryModel CategoryModel { get; set; }
        public List<ProductModel> ProductModels { get; set; } = new List<ProductModel>();
        public PageInfo PageInfo { get; set; } = new PageInfo();

    }
}
