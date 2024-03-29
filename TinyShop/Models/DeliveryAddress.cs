using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
#nullable enable
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

        public override bool Equals( object? obj )
        {
            if ( obj == null || GetType() != obj.GetType() )
            {
                return false;
            }
            DeliveryAddress? target = obj as DeliveryAddress;
            return TheRegion?.Id == target?.TheRegion?.Id
                && TheCity?.Id == target?.TheCity?.Id
                && TheWarehouse?.Id == target?.TheWarehouse?.Id;
        }

        public override int GetHashCode()
        {
            return ( TheRegion?.Id + TheCity?.Id + TheWarehouse?.Id ).GetHashCode();
        }
    }
#nullable disable
}
