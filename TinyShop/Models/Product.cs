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

        [Required, StringLength( 256, MinimumLength = 3 ), Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength( 4096 ), Display( Name = "Description" )]
        public string Description { get; set; }

        [DataType( DataType.Currency ), Column( TypeName = "decimal(18, 2)" ), Display( Name = "Price" )]
        public decimal? Price { get; set; }

        [Display( Name = "Group" )]
        public int ProductGroupId { get; set; }

        [Display( Name = "Group" )]
        public virtual ProductGroup ProductGroup { get; set; }

        public virtual ICollection<FileTag> DescImages { get; set; }        

        [NotMapped]
        public FileTag MainImage
        {
            get
            {                
                if ( DescImages.Count > 0 )
                    return DescImages.ElementAt( 0 );
                return null;
            }
        }        
    }
}
