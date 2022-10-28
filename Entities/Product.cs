using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    //entity for Products table in database
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductUrl { get; set; }
        //public string ImageUrl { get; set; }
        public string MainImageUrl { get; set; }
        public string CardDescription { get; set; }
        public string Description { get; set; }
        //public double BaseDolarPrice { get; set; }
        public double BasePrice { get; set; }
        public BasePriceType BasePriceType { get; set; }
        public double SalePrice { get; set; }
        public int Quantity { get; set; }
        public int ViewCount { get; set; } = 0;
        public int PurchaseCount { get; set; } = 0;
        public bool IsApproved { get; set; } = false;
        public bool IsSuggested { get; set; } = false;
        public bool IsPopular { get; set; } = false;
        public bool IsAltered { get; set; } = false;
        public DateTime DateAdded { get; set; }
        public StockState StockState { get; set; } = StockState.notAvailable;
        public List<ProductCategory> ProductCategories { get; set; } // navigation property for product categories
        public List<ProductImage> ProductImages { get; set; } // navigation property for product images

        public void SetStockState(int stockState)
        {
            if (stockState == 1)
            {
                StockState = StockState.available;
            }
            else if (stockState == 2)
            {
                StockState = StockState.onOrder;
            }
            else
            {
                StockState = StockState.notAvailable;
            }
        }

        public void SetBasePriceType(int basePriceType)
        {
            if (basePriceType == 1)
            {
                BasePriceType = BasePriceType.Dollar;
            }
            else if (basePriceType == 2)
            {
                BasePriceType = BasePriceType.Euro;
            }
            else
            {
                BasePriceType = BasePriceType.DontChange;
            }
        }

    }

    public enum StockState
    {
        available = 1,
        onOrder = 2,
        notAvailable = 3

    }

    public enum BasePriceType
    {
        Dollar = 1,
        Euro = 2,
        DontChange = -1
    }
}
