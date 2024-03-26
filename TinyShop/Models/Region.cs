using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class Region : JsonSupportable
    {
        [Required, StringLength( LengthLarge ), Display( Name = "Область") ]
        public string Name { get; set; } = string.Empty;

        public DeliveryFirm? TheDeliveryFirm { get; set; }
    }
}
