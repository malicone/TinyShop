using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    public class ProductGroup : SoftDeletableEntity
    {
        [Required, StringLength( LengthMedium, MinimumLength = 3 ), Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
