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

        [StringLength( LENGTH_MEDIUM )]
        public string Name { get; set; }

        [StringLength( LENGTH_SMALL_EXTRA )]
        public string Ext { get; set; }

        public byte[] Body { get; set; }

        public long? Length { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
