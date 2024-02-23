using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json.Serialization;
using TinyShop.Infrastructure;

namespace TinyShop.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
            SessionCart cart = session?.GetJson<SessionCart>( GlobalConstants.CartSessionId ) ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem( product, quantity );
            Session?.SetJson( GlobalConstants.CartSessionId, this );
        }
        public override void RemoveLine(Product product)
        {
            base.RemoveLine( product );
            Session?.SetJson( GlobalConstants.CartSessionId, this );
        }
        public override void Clear()
        {
            base.Clear();
            Session?.Remove( GlobalConstants.CartSessionId );
        }
    }
}
