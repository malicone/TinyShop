using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class DeliveryFirm : SoftDeletableEntity
    {
        [Required, StringLength(LENGTH_MEDIUM)]
        public string Name { get; set; }
        public int SortingColumn { get; set; }
    }
}