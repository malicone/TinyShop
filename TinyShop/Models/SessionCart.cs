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
            SessionCart cart = session?.GetJson<SessionCart>( _SessionId ) ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }
        public override void AddItem(Product product, int quantity = 1)
        {
            base.AddItem( product, quantity );
            Session?.SetJson( _SessionId, this );
        }
        public override void RemoveItem(int productId, int quantity = 1)
        {
            base.RemoveItem( productId, quantity );
            Session?.SetJson( _SessionId, this );
        }
        public override void RemoveLine(int productId)
        {
            base.RemoveLine( productId );
            Session?.SetJson( _SessionId, this );
        }
        public override void Clear()
        {
            base.Clear();
            Session?.Remove( _SessionId );
        }

        private static string _SessionId { get { return "cart"; } }
    }
}
