using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class DeliveryAddress : SoftDeletableEntity
    {
        public Region? TheRegion { get; set; } = new Region();
        [StringLength( LengthLarge )]
        public string? RegionNameSnapshot { get; set; } = string.Empty;

        public City? TheCity { get; set; } = new City();
        [StringLength( LengthLarge )]
        public string? CityNameSnapshot { get; set; } = string.Empty;

        public Warehouse? TheWarehouse { get; set; } = new Warehouse();
        [StringLength( LengthLarge )]
        public string? WarehouseNameSnapshot { get; set; } = string.Empty;

        // AddressLine1, AddressLine2 for courier delivery can be added here in the future
    }
}
