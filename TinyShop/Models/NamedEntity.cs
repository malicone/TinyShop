using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class NamedEntity : SoftDeletableEntity
    {
        [Required, StringLength( StrLengthMedium )]
        public string Name { get; set; } = string.Empty;
        public int SortingColumn { get; set; }
    }
}
