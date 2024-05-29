using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Models
{
#nullable enable
    public class WarehouseType : JsonSupportable
    {
        [Required, StringLength( StrLengthMedium )]
        public string Name { get; set; } = string.Empty;

        public DeliveryFirm? TheDeliveryFirm { get; set; }

        [NotMapped]
        public static int NovaposhtaWarehouse30kgTypeId { get { return 2; } }
        [NotMapped]
        public static int NovaposhtaWarehouse1000kgTypeId { get { return 4; } }
        [NotMapped]
        public static int NovaposhtaWarehousePoshtomatTypeId { get { return 5; } }
    }
#nullable disable
}
