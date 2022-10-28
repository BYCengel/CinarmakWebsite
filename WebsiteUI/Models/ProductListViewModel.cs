using Entities;

namespace WebsiteUI.Models
{
    public class ProductListViewModel
    {
        public string SearchString { get; set; } = "";
        public string CategoryUrl { get; set; }
        public string CurrentAction { get; set; } = "";
        public PageInfo PageInfo { get; set; }
        public List<ProductModel> ProductModels { get; set; } = new List<ProductModel>();
    }
}
