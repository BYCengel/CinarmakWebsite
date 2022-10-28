using WebsiteUI.Models;

namespace WebsiteUI.ViewModels
{
    public class HomePageViewModel
    {
        public List<ProductModel> PopularProductModels { get; set; } = new List<ProductModel>();
        public List<ProductModel> SuggestedProductModels { get; set; } = new List<ProductModel>();
        public List<ImageModel> HomeDisplayImageModels { get; set; } = new List<ImageModel>();
    }
}
