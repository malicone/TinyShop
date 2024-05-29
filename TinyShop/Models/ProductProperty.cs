﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
#nullable enable
    public class ProductProperty : NamedEntity
    {
        [StringLength(StrLengthMedium)]
        public string? Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductPropertyItem> Items { get; set; }
    }
#nullable disable
}
