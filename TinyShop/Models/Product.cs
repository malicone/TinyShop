using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    public class Product : BaseEntity
    {
        public Product()
        {
            DescImages = new HashSet<FileTag>();
        }

        [Required, StringLength( 256, MinimumLength = 3 )]
        public string Name { get; set; }

        [StringLength( 4096 )]
        public string Description { get; set; }

        [DataType( DataType.Currency ), Column( TypeName = "decimal(18, 2)" )]
        public decimal? Price { get; set; }

        public int ProductGroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }

        public virtual ICollection<FileTag> DescImages { get; set; }
    }
}
