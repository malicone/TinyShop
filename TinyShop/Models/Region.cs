using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace TinyShop.Models
{
#nullable enable
    [DebuggerDisplay( "{Name}" )]
    public class Region : JsonSupportable
    {
        [Required, StringLength( LengthLarge ), Display( Name = "Область") ]
        public string Name { get; set; } = string.Empty;

        public DeliveryFirm? TheDeliveryFirm { get; set; }
    }
#nullable disable
}
