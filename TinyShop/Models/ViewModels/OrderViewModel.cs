using System.Collections.Generic;

namespace TinyShop.Models.ViewModels
{
    public class OrderViewModel
    {
        public Order TheOrder { get; set; } = new Order();
        public List<PaymentType> PaymentTypes { get; set; } = new List<PaymentType>();
        public List<DeliveryType> DeliveryTypes { get; set; } = new List<DeliveryType>();
    }
}
