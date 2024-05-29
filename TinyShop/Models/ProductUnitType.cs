using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TinyShop.Models
{
#nullable enable
    public class ProductUnitType : NamedEntity
    {
        [StringLength(StrLengthMedium)]
        public string? Description { get; set; }

        [NotMapped]
        public const int UnitId = 1;
        [NotMapped]
        public const int PairId = 2;
    }
#nullable disable
}
