using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class WarehouseType : JsonSupportable
    {
        [StringLength( LENGTH_MEDIUM )]
        public string Name { get; set; }

        public DeliveryType TheDeliveryType { get; set; }        
    }
}
