using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class PaymentType : BaseEntity
    {
        [Required, Display( Name = "Спосіб оплати" ), StringLength( LengthSmall )]
        public string Name { get; set; } = string.Empty;
    }
}
