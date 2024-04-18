using System.Collections.Generic;
using System.Linq;

namespace TinyShop.Models
{
    public class Cart
    {
        public List<OrderLine> Lines { get; set; } = new List<OrderLine>();
        public virtual void AddItem(Product product, int quantity = 1)
        {
            OrderLine? line = Lines.Where( p => p.TheProduct.Id == product.Id ).FirstOrDefault();
            if (line == null)
            {
                Lines.Add( new OrderLine
                {
                    TheProduct = product,
                    Quantity = quantity,
                    PriceSnapshot = product.Price.Value
                } );
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveItem( int productId, int quantity = 1 )
        {
            OrderLine? line = Lines.Where( p => p.TheProduct.Id == productId ).FirstOrDefault();
            if ( line != null )
            {
                line.Quantity = line.Quantity - quantity;
                if ( line.Quantity <= 0 )
                {
                    RemoveLine( productId );
                }
            }
        }
        public virtual void RemoveLine( int productId ) => Lines.RemoveAll( l => l.TheProduct.Id == productId );
        public decimal ComputeTotalValue() => (decimal)Lines.Sum( e => e.PriceSnapshot * e.Quantity );
        public int ComputeTotalQuantity() => Lines.Sum( e => e.Quantity );

        public virtual void Clear() => Lines.Clear();
    }
}
