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

    public class Order : SoftDeletableEntity
    {
        /// <summary>
        /// Important! These are values got from Nova Poshta api - https://api.novaposhta.ua/v2.0/json/
        /// calledMethod : getAreas
        /// </summary>
        [NotMapped]
        public static string NovaPoshtaVolynRegionId { get { return "7150812a-9b87-11de-822f-000c2965ae0e"; } }
        public static string NovaPoshtaDefaultRegionId { get { return NovaPoshtaVolynRegionId; } }
        public DateTime OrderDateTime { get; set; }
        public virtual Customer TheCustomer { get; set; } = new();
        public virtual OrderStatus TheOrderStatus { get; set; } = new();
        public virtual DeliveryType TheDeliveryType { get; set; } = new();
        public virtual PaymentType ThePaymentType { get; set; } = new();

        [Display( Name = "Область" ), StringLength(512)]
        public string RegionId { get; set; }

        [Display( Name = "Місто/село" ), StringLength(512)]
        public string CityId { get; set; }

        [Display( Name = "Відділення" ), StringLength( 512 )]
        public string WarehouseId { get; set; }

        [Display( Name = "Коментарій до замовлення" )]
        public string Comments { get; set; }

        [BindNever]
        public virtual ICollection<OrderLine> Lines { get; set; } = new List<OrderLine>();
    }
}