using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        //Navigation Properties

        public List<CartItem> CartItems { get; set; }
    }
}
