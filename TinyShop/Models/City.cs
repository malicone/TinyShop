using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class City : JsonSupportable
    {
        [StringLength( LENGTH_LARGE )]
        public string Name { get; set; }

        [StringLength( LENGTH_SMALL )]
        public string TypeDescription { get; set; }

        [StringLength( LENGTH_SMALL )]
        public string Index { get; set; }

        [StringLength( LENGTH_ID_EXTERNAL )]
        public string RegionIdExternal { get; set; }

        public Region TheRegion { get; set; }

        public DeliveryType? TheDeliveryType { get; set; }
    }
}
