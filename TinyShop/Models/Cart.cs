using System.Collections.Generic;
using System.Linq;

namespace TinyShop.Models
{
    public class Cart
    {
        public List<OrderLine> Lines { get; set; } = new List<OrderLine>();
        public virtual void AddItem(Product product, int quantity)
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
        public virtual void RemoveLine(Product product) => Lines.RemoveAll( l => l.TheProduct.Id == product.Id );
        public decimal ComputeTotalValue() => (decimal)Lines.Sum( e => e.TheProduct.Price * e.Quantity );
        public int ComputeTotalQuantity() => Lines.Sum( e => e.Quantity );

        public virtual void Clear() => Lines.Clear();
    }
}
