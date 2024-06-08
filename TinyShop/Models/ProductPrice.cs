using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Models
{
    /// <summary>
    /// Product can have multiple prices for different pack sizes.
    /// </summary>
    public class ProductPrice : SoftDeletableEntity
    {
        public Product TheProduct { get; set; }
        
        [Required, DataType( DataType.Currency ), Column( TypeName = "decimal(18, 2)" )]
        public decimal PricePerUnit { get; set; }
        
        public int UnitsPerPack { get; set; }

        [NotMapped]
        public decimal PackPrice => PricePerUnit * UnitsPerPack;

        public int MinPackSaleQuantity { get; set; }
        
        [NotMapped]
        public decimal TotalPrice => PackPrice * MinPackSaleQuantity;
    }
}
