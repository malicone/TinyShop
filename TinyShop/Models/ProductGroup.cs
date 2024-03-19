using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    public class ProductGroup : SoftDeletableEntity
    {
        [Required, StringLength( LENGTH_MEDIUM, MinimumLength = 3 ), Display(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
