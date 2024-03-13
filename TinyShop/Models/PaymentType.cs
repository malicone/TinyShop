using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class PaymentType : BaseEntity
    {
        [Display( Name = "Спосіб оплати" ), StringLength( 64 )]
        public string Name { get; set; }
    }
}
