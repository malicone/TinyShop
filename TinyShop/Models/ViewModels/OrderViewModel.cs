using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models.ViewModels
{
    public class OrderViewModel
    {
        // We can use this property to bind the form data to the Order object but then
        // all the nested required properties will be validated. For example OrderStatus.Name and we
        // will get validation errors.
        //public Order TheOrder { get; set; } = new Order();
        public int? OrderId { get; set; }
        public decimal OrderTotalSum { get; set; }
        public Customer TheCustomer { get; set; } = new Customer();
        [Display(Name = "Доставка")]
        public int DeliveryTypeId { get; set; }
        [Display(Name = "Область")]
        public int? RegionId { get; set; }
        [Display(Name = "Місто/село")]
        public int? CityId { get; set; }
        [Display(Name = "Відділення")]
        public int? WarehouseId { get; set; }
        [Display(Name = "Оплата")]
        public int PaymentTypeId { get; set; }
        [Display(Name = "Коментар")]
        public string? Comments { get; set; } = string.Empty;
        public List<PaymentType> PaymentTypes { get; set; } = new List<PaymentType>();
        public List<DeliveryType> DeliveryTypes { get; set; } = new List<DeliveryType>();
    }
}
