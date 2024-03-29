using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
#nullable enable
    [Index( nameof( CityIdExternal ) )]
    [Index( nameof( WarehouseTypeIdExternal ) )]
    public class Warehouse : JsonSupportable
    {
        public Warehouse()
        {
            TheCity = new City();
            TheDeliveryFirm = new DeliveryFirm();
            Name = string.Empty;
        }

        [Required, StringLength( LengthLarge ), Display( Name = "Відділення" )]
        public string Name { get; set; } = string.Empty;

        [StringLength( LengthIdExturnal )]
        public string? CityIdExternal { get; set; }

        [StringLength( LengthIdExturnal )]
        public string? WarehouseTypeIdExternal { get; set; }

        public City? TheCity { get; set; }

        public DeliveryFirm? TheDeliveryFirm { get; set; }

        public WarehouseType? TheWarehouseType { get; set; }
    }
#nullable disable
}
