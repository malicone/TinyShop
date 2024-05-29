using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
#nullable enable
    public class DeliveryAddress : SoftDeletableEntity
    {
        public Region? TheRegion { get; set; }
        [StringLength( StrLengthLarge )]
        public string? RegionNameSnapshot { get; set; }

        public City? TheCity { get; set; }
        [StringLength( StrLengthLarge )]
        public string? CityNameSnapshot { get; set; }

        public Warehouse? TheWarehouse { get; set; }
        [StringLength( StrLengthLarge )]
        public string? WarehouseNameSnapshot { get; set; }

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
