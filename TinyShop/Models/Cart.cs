using System.Collections.Generic;
using System.Linq;

namespace TinyShop.Models
{
    public class Cart
    {
        public List<OrderLine> Lines { get; set; } = new List<OrderLine>();
        public virtual void AddItem(Product product, int quantity = 1)
        {
            OrderLine line = Lines.FirstOrDefault(p => p.TheProduct.Id == product.Id);
            if (line == null)
            {
                ProductPrice price = GetMatchingPrice(product, quantity);
                Lines.Add( new OrderLine
                {
                    TheProduct = product,
                    Quantity = quantity,
                    PricePerUnitSnapshot = price.PricePerUnit,
                    UnitsPerPackSnapshot = price.UnitsPerPack,
                    MinPackSaleQuantitySnapshot = price.MinPackSaleQuantity,
                } );
            }
            else
            {
                line.Quantity += quantity;
                ProductPrice price = GetMatchingPrice( product, line.Quantity );
                line.PricePerUnitSnapshot = price.PricePerUnit;
                line.UnitsPerPackSnapshot = price.UnitsPerPack;
                line.MinPackSaleQuantitySnapshot = price.MinPackSaleQuantity;
            }
        }
        private ProductPrice GetMatchingPrice(Product product, int quantity)
        {
            return product.GetMatchingPrice(quantity);
        }
        public virtual void RemoveItem( int productId, int quantity = 1 )
        {
            OrderLine line = Lines.FirstOrDefault(p => p.TheProduct.Id == productId);
            if ( line != null )
            {
                line.Quantity -= quantity;
                if ( line.Quantity <= 0 )
                {
                    RemoveLine( productId );
                }
                else
                {
                    ProductPrice price = GetMatchingPrice( line.TheProduct, line.Quantity );
                    line.PricePerUnitSnapshot = price.PricePerUnit;
                    line.UnitsPerPackSnapshot = price.UnitsPerPack;
                    line.MinPackSaleQuantitySnapshot = price.MinPackSaleQuantity;
                }
            }
        }
        public virtual void RemoveLine( int productId ) => Lines.RemoveAll( l => l.TheProduct.Id == productId );
        public decimal ComputeTotalValue() => Lines.Sum( e => e.PackPriceSnapshot * e.Quantity );
        public int ComputeTotalQuantity() => Lines.Sum( e => e.Quantity );

        public virtual void Clear() => Lines.Clear();
    }
}