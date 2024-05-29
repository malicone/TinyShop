﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
#nullable enable
    public class FileTag : BaseEntity
    {
        public FileTag()
        {
            Products = new HashSet<Product>();
        }

        [Required, StringLength( StrLengthMedium )]
        public string Name { get; set; } = string.Empty;

        [StringLength( StrLengthSmallExtra )]
        public string? Ext { get; set; }

        public byte[] Body { get; set; }

        public long? Length { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
#nullable disable
}
