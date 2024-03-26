using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Models
{
    public class DeliveryType : SoftDeletableEntity
    {
        [Required, Display( Name = "Спосіб доставки"), StringLength( LengthSmall )]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DeliveryFirm TheDeliveryFirm { get; set; } = new DeliveryFirm();

        [Required]
        public int SortingColumn { get; set; }

        /// <summary>
        /// Important! These are values from the database.
        /// </summary>
        [NotMapped]        
        public static int PickupId { get { return 1; } }

        [NotMapped]
        public static int NovaPoshtaWarehouseId { get { return 2; } }
    }
}
