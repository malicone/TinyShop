using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Models
{
    public class DeliveryType : BaseEntity
    {
        [Display( Name = "Спосіб доставки"), StringLength( 64 )]
        public string Name { get; set; }

        /// <summary>
        /// Important! These are values from the database.
        /// </summary>
        [NotMapped]        
        public static int PickupId { get { return 1; } }

        [NotMapped]
        public static int NovaPoshtaWarehouseId { get { return 2; } }
    }
}
