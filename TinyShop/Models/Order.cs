using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TinyShop.RestUtils.Common.Dto;

namespace TinyShop.Models
{
    public class OrderLine : SoftDeletableEntity
    {        
        public virtual Order TheOrder { get; set; } = new();
        public virtual Product TheProduct { get; set; } = new();

        [DataType( DataType.Currency ), Column( TypeName = "decimal(18, 2)" )]
        public decimal? PriceSnapshot { get; set; }

        public int Quantity { get; set; }
    }

    public enum DeliveryType
    {
        [Display( Name = "Pickup" )]
        Pickup,
        [Display( Name = "Nova poshta warehouse" )]
        NovaPoshtaWarehouse
    }
    public enum PaymentType
    {
        [Display( Name = "Card" )]
        Card,
        [Display( Name = "Imposed" )]
        Imposed
    }
    public enum OrderStatus
    {
        [Display( Name = "New" )]
        New,
        [Display( Name = "In progress" )]
        InProgress,
        [Display( Name = "Completed" )]
        Completed,
        [Display( Name = "Canceled" )]
        Canceled
    }

    public class Order : SoftDeletableEntity
    {
        public DateTime OrderDateTime { get; set; }
        public virtual Customer TheCustomer { get; set; } = new();

        [BindNever]
        public virtual ICollection<OrderLine> Lines { get; set; } = new List<OrderLine>();

        [Display( Name = "Region" )]
        public string RegionId { get; set; }

        [Display( Name = "City" )]
        public string CityId { get; set; }

        [Display( Name = "Warehouse" )]
        public string WarehouseId { get; set; }

        public string Comments { get; set; }
    }
}