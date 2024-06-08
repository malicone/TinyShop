using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace TinyShop.Models
{
    [DebuggerDisplay( "{Name}" )]
    public class Region : JsonSupportable
    {
        [Required, StringLength( StrLengthLarge ), Display( Name = "Область") ]
        public string Name { get; set; } = string.Empty;

        public DeliveryFirm? TheDeliveryFirm { get; set; }

        /// <summary>
        /// Important! Values from the db
        /// </summary>
        [NotMapped]
        public static int NovaPoshtaVolynRegionId { get { return 22; } }
        [NotMapped]
        public static int NovaPoshtaDefaultRegionId { get { return NovaPoshtaVolynRegionId; } }
    }
}
