using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.UserEntities.SiteUser
{
    public class Address
    {
        public int Id { get; set; }
        public int PostalCode { get; set; }
        public string AddressName { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AddressBody { get; set; }
        public string AddressNote { get; set; }

        //Navigation Properties

        public SiteUser User { get; set; }
    }
}
