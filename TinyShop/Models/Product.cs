using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Required, StringLength( StrLengthMedium, MinimumLength = 3 ), Display(Name = "Назва")]
        public string Name { get; set; } = string.Empty;

        [Display( Name = "Опис" )]
        public string? Description { get; set; }

        [Required, Display( Name = "Група" )]
        public int ProductGroupId { get; set; }

        [Required, Display( Name = "Група" )]
        public virtual ProductGroup ProductGroup { get; set; }

        public virtual ICollection<FileTag> DescImages { get; set; }        
        public virtual ICollection<ProductProperty> Properties { get; set; }
        public virtual ICollection<ProductPropertyItem> PropertyItems { get; set; }

        [DefaultValue(ProductUnitType.UnitId)]
        public int UnitTypeId { get; set; }
        public ProductUnitType UnitType { get; set; }

        [NotMapped]
        public FileTag? MainImage
        {
            get
            {                
                if ( DescImages.Count > 0 )
                    return DescImages.ElementAt( 0 );
                return null;
            }
        }        
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public virtual IList<ProductPrice> Prices { get; set; } = new List<ProductPrice>();

        /// <summary>
        /// Deprecated. Use Prices and others instead.
        /// </summary>
        [DataType( DataType.Currency ), Column( TypeName = "decimal(18, 2)" ), Display( Name = "Ціна" )]
        public decimal? Price { get; set; }

        /// <summary>
        /// These prices properties are just for convenience. They are not stored in the database and use
        /// Prices[0] element.
        /// </summary>
        [NotMapped]
        public decimal PricePerUnit
        {
            get
            {
                if(Prices != null)
                {
                    if(Prices.Count > 0)
                        return Prices.ElementAt(0).PricePerUnit;
                }
                return 0;
            }
        }

        [NotMapped]
        public int UnitsPerPack
        {
            get
            {
                if(Prices != null)
                {
                    return Prices.Count > 0 ? Prices.ElementAt(0).UnitsPerPack : 0;
                }
                return 0;
            }
        }

        [NotMapped]
        public decimal PackPrice => PricePerUnit * UnitsPerPack;

        [NotMapped]
        public int MinPackSaleQuantity
        {
            get
            {
                if(Prices != null)
                {
                    return Prices.Count > 0 ? Prices.ElementAt( 0 ).MinPackSaleQuantity : 0;
                }
                return 0;
            }
        }

        [NotMapped]
        public decimal TotalPrice => PackPrice * MinPackSaleQuantity;

        [NotMapped]
        public int PriceCount
        {
            get
            {
                if(Prices == null)
                {
                    return 0;
                }
                return Prices.Count;
            }
        }

        /// <summary>
        /// We can have specified wholesale prices for the product or it can be negotiable.
        /// </summary>
        [NotMapped]
        public bool WholesalePriceAvailable
        {
            get
            {
                if(WholesalePriceNegotiable)
                {
                    return true;
                }
                if(Prices == null)
                {
                    return false;
                }
                return Prices.Count > 1;
            }
        }

        public bool WholesalePriceNegotiable { get; set; }
        public ProductPrice GetMatchingPrice(int quantity)
        {
            if((Prices == null) || (Prices.Count == 0))
            {
                return new ProductPrice();
            }
            ProductPrice currentPrice = Prices.ElementAt(0);
            for(int i = 1; i < Prices.Count; i++)
            {
                if(quantity >= Prices.ElementAt(i).MinPackSaleQuantity)
                {
                    currentPrice = Prices.ElementAt(i);
                }
                else
                {
                    return currentPrice;
                }
            }
            return currentPrice;
        }
    }
}
