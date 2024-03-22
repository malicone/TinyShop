using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class Region : JsonSupportable
    {
        [StringLength( LENGTH_LARGE ), Display( Name = "Область") ]
        public string Name { get; set; }

        public DeliveryType TheDeliveryType { get; set; }
    }
}
