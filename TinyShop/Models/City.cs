using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
#nullable enable
    [Index( nameof( RegionIdExternal ) )]
    public class City : JsonSupportable
    {
        [Required, StringLength( LengthLarge ), Display(Name = "Місто/село")]
        public string Name { get; set; } = string.Empty;

        [StringLength( LengthSmall )]
        public string? TypeDescription { get; set; }

        [StringLength( LengthIdExturnal )]
        public string? RegionIdExternal { get; set; }

        public Region? TheRegion { get; set; }
        
        public DeliveryFirm? TheDeliveryFirm { get; set; }
        
        public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
#nullable disable
    }
}
