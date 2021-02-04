using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    public class ProductGroup : BaseEntity
    {
        [Required, StringLength( 256, MinimumLength = 3 ), Display(Name = "Назва")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
