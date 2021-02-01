using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    public class FileTag : BaseEntity
    {
        public FileTag()
        {
            Products = new HashSet<Product>();
        }

        [StringLength( 256 )]
        public string Name { get; set; }

        [StringLength( 32 )]
        public string Ext { get; set; }

        public byte[] Body { get; set; }

        public long? Length { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
