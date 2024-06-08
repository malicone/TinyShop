using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TinyShop.Models
{
    public class OrderLine : SoftDeletableEntity
    {
        [Required]
        public virtual Order TheOrder { get; set; }
        [Required]
        public virtual Product TheProduct { get; set; }

        /// <summary>
        /// Price at the moment of order creation. Deprecated. Use ProductPrice instead.
        /// </summary>
        [Required, DataType( DataType.Currency ), Column( TypeName = "decimal(18, 2)" )]
        public decimal PriceSnapshot { get; set; }

        public decimal PricePerUnitSnapshot { get; set; }
        public int UnitsPerPackSnapshot { get; set; }
        [NotMapped]
        public decimal PackPriceSnapshot => PricePerUnitSnapshot * UnitsPerPackSnapshot;
        public int MinPackSaleQuantitySnapshot { get; set; }
        [NotMapped]
        public decimal TotalPriceSnapshot => PackPriceSnapshot * MinPackSaleQuantitySnapshot;

        [Required]                                             
        public int Quantity { get; set; }

        public virtual ICollection<ProductPropertyItem> PropertyItems { get; set; }
    }

    public class Order : SoftDeletableEntity
    {
        [Display( Name = "Дата замовлення" )]
        public DateTime OrderDateTime { get; set; }
        
        [Required]
        public virtual Customer TheCustomer { get; set; }
        public virtual OrderStatus? TheOrderStatus { get; set; }

        [Required]
        public virtual DeliveryType TheDeliveryType { get; set; }

        [Required]
        public virtual PaymentType ThePaymentType { get; set; }
        public virtual DeliveryAddress? TheDeliveryAddress { get; set; }

        [Display( Name = "Коментар до замовлення" )]
        public string? Comments { get; set; }

        [BindNever]
        public virtual ICollection<OrderLine> Lines { get; set; }

        public decimal ComputeTotalValue() => Lines.Sum( e => e.PackPriceSnapshot * e.Quantity );
        public int ComputeTotalQuantity() => Lines.Sum( e => e.Quantity );
    }
}