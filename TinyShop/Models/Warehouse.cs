using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    [Index( nameof( CityIdExternal ) )]
    [Index( nameof( WarehouseTypeIdExternal ) )]
    public class Warehouse : JsonSupportable
    {
#nullable enable
        public Warehouse()
        {
            TheCity = new City();
            TheDeliveryType = new DeliveryType();
            Name = string.Empty;
        }

        [StringLength( LENGTH_LARGE ), Display( Name = "Відділення" )]
        public string Name { get; set; }

        [StringLength( LENGTH_ID_EXTERNAL )]
        public string? CityIdExternal { get; set; }

        [StringLength( LENGTH_ID_EXTERNAL )]
        public string? WarehouseTypeIdExternal { get; set; }

        public City TheCity { get; set; }

        public DeliveryType TheDeliveryType { get; set; }

        public WarehouseType? TheWarehouseType { get; set; }
#nullable disable
    }
}
