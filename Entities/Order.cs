using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }

        //Navigation Properties

        public List<OrderItem> OrderItems { get; set; }
        public int AddressId { get; set; }

        //Payment Infos
        //DO NOT STORE SENSITIVE INFORMATION

        public string PaymentId { get; set; }
        public string ConversationId { get; set; }
        public EnumPaymentTypes PaymentType { get; set; }

    }

    public enum EnumOrderState
    {
        waiting = 0,
        unpaid = 1,
        completed = 2
    }

    public enum EnumPaymentTypes
    {
        CreditCard = 0,
        Eft = 1,
        Cash = 2
    }
}
