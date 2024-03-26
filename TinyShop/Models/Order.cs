using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TinyShop.RestUtils.Common.Dto;

namespace TinyShop.Models
{
    public class OrderLine : SoftDeletableEntity
    {
        [Required]
        public virtual Order TheOrder { get; set; } = new();
        [Required]
        public virtual Product TheProduct { get; set; } = new();

        [Required, DataType( DataType.Currency ), Column( TypeName = "decimal(18, 2)" )]
        public decimal PriceSnapshot { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

    public class Order : SoftDeletableEntity
    {
        /// <summary>
        /// Important! Values from the db
        /// </summary>
        [NotMapped]
        public static int NovaPoshtaVolynRegionId { get { return 22; } }
        [NotMapped]
        public static int NovaPoshtaDefaultRegionId { get { return NovaPoshtaVolynRegionId; } }
        [Display( Name = "Дата замовлення" )]
        public DateTime OrderDateTime { get; set; }
        public virtual Customer TheCustomer { get; set; } = new();
        public virtual OrderStatus? TheOrderStatus { get; set; } = new();
        public virtual DeliveryType TheDeliveryType { get; set; } = new();
        public virtual PaymentType ThePaymentType { get; set; } = new();
#nullable enable
        public virtual DeliveryAddress? TheDeliveryAddress { get; set; } = new();
#nullable disable

        [Display( Name = "Коментар до замовлення" )]
        public string? Comments { get; set; }

        [BindNever]
        public virtual ICollection<OrderLine> Lines { get; set; } = new List<OrderLine>();

        public decimal ComputeTotalValue() => Lines.Sum( e => e.PriceSnapshot * e.Quantity );
        public int ComputeTotalQuantity() => Lines.Sum( e => e.Quantity );
    }
}