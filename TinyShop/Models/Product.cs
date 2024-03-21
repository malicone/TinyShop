using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    public class Product : SoftDeletableEntity
    {
        public Product()
        {
            DescImages = new HashSet<FileTag>();
        }

        [Required, StringLength( LENGTH_MEDIUM, MinimumLength = 3 ), Display(Name = "Name")]
        public string Name { get; set; }

        [Display( Name = "Опис" )]
        public string Description { get; set; }

        [DataType( DataType.Currency ), Column( TypeName = "decimal(18, 2)" ), Display( Name = "Ціна" )]
        public decimal? Price { get; set; }

        [Display( Name = "Група" )]
        public int ProductGroupId { get; set; }

        [Display( Name = "Група" )]
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
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
