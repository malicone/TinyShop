using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
#nullable enable
    public class NamedEntity : SoftDeletableEntity
    {
        [Required, StringLength( LengthMedium )]
        public string Name { get; set; } = string.Empty;
        public int SortingColumn { get; set; }
    }
#nullable disable
}
