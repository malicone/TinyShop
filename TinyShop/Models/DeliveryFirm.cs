using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class DeliveryFirm : SoftDeletableEntity
    {
        [Required, StringLength(LengthMedium)]
        public string Name { get; set; } = string.Empty;
        public int SortingColumn { get; set; }
    }
}