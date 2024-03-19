using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class PaymentType : BaseEntity
    {
        [Display( Name = "Спосіб оплати" ), StringLength( LENGTH_SMALL )]
        public string Name { get; set; }
    }
}
