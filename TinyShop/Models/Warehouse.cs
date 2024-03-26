using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
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

        [StringLength( LENGTH_ID_EXTERNAL )]
        public string? CityIdExternal { get; set; }

        [StringLength( LENGTH_ID_EXTERNAL )]
        public string? WarehouseTypeIdExternal { get; set; }

        public City? TheCity { get; set; }

        public DeliveryFirm? TheDeliveryFirm { get; set; }

        public WarehouseType? TheWarehouseType { get; set; }
    }
}
