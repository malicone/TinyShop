using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Models
{
    public class DeliveryFirm : NamedEntity
    {
        /// <summary>
        /// These are values from db.
        /// </summary>
        [NotMapped]
        public static int NovaPoshtaId { get; } = 2;
    }
}