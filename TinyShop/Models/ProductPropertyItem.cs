using System.Collections.Generic;

namespace TinyShop.Models
{
#nullable enable
    public class ProductPropertyItem : NamedEntity
    {
        public ProductProperty TheProductProperty { get; set; }
        public FileTag? TheFileTag { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
#nullable disable
}
