using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    // entity for category table in database
    public class Category
    {
        public int Id { get; set; }
        public int ViewCount { get; set; } = 0;
        public string Name { get; set; }
        public string CategoryUrl { get; set; }
        public List<ProductCategory> ProductCategories { get; set; } // navigation property for product categories
    }
}
