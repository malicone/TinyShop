using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class OrderStatus : BaseEntity
    {
        [StringLength( 64 )]
        public string Name { get; set; }
    }
}
