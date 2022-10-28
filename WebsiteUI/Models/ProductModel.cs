using Entities;
using System.ComponentModel.DataAnnotations;

namespace WebsiteUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public string CardDescription { get; set; }
        public string ProductUrl { get; set; } = "";
        public string MainImageUrl { get; set; } = "";
        //public string ImageUrl { get; set; } = "";
        //public double BaseDolarPrice { get; set; }
        public double BasePrice { get; set; }
        public double SalePrice { get; set; }
        public int Quantity { get; set; } = 0;
        public int ViewCount { get; set; } = 0;
        public int PurchaseCount { get; set; } = 0;
        public bool IsApproved { get; set; } = false;
        public bool IsHomeDisplay { get; set; } = false;
        public bool IsPopular { get; set; } = false;
        public bool IsStaticPopular { get; set; } = false;
        public int StockStateNumber { get; set; } = 3;
        public int BasePriceTypeNumber { get; set; } = -1;
        public List<Category> SelectedCategories { get; set; } = new List<Category>();
        public List<ImageModel> ImageModels { get; set; } = new List<ImageModel>();

    }
}
