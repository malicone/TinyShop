using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    public class ProductGroup : SoftDeletableEntity
    {
        [Required, StringLength( StrLengthMedium, MinimumLength = 3 ), Display(Name = "Назва")]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; }

        [NotMapped]
        // Used in HomeController.Index
        public int ProductCount { get; set; } = 0;
    }
}
