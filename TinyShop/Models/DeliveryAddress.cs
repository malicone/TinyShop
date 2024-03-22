using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class DeliveryAddress : SoftDeletableEntity
    {
#nullable enable
        public Region? TheRegion { get; set; } = new Region();
        [StringLength( LENGTH_LARGE )]
        public string? RegionNameSnapshot { get; set; } = string.Empty;

        public City? TheCity { get; set; } = new City();
        [StringLength( LENGTH_LARGE )]
        public string? CityNameSnapshot { get; set; } = string.Empty;

        public Warehouse? TheWarehouse { get; set; } = new Warehouse();
        [StringLength( LENGTH_LARGE )]
        public string? WarehouseNameSnapshot { get; set; } = string.Empty;

        // AddressLine1, AddressLine2 for courier delivery can be added here in the future
#nullable disable
    }
}
