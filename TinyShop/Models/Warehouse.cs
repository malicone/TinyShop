using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class Warehouse : JsonSupportable
    {
        [StringLength( LENGTH_LARGE )]
        public string Name { get; set; }

        [StringLength( LENGTH_ID_EXTERNAL )]
        public string CityIdExternal { get; set; }

        public City TheCity { get; set; }

        public DeliveryType? TheDeliveryType { get; set; }
    }
}
