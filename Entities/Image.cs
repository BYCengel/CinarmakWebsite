using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsHomeDisplay { get; set; } = false;
        public bool IsMainImageOfProduct { get; set; } = false;
        public List<ProductImage> ProductImages { get; set; }
    }
}
