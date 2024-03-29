using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
#nullable enable
    public class ProductGroup : SoftDeletableEntity
    {
        [Required, StringLength( LengthMedium, MinimumLength = 3 ), Display(Name = "Назва")]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
#nullable disable
}
