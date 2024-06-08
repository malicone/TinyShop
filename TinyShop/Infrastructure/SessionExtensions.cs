using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TinyShop.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve// to avoid circular references
            };
            session.SetString( key, JsonSerializer.Serialize( value, options ) );
        }
        public static T? GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString( key );
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            return sessionData == null ? default( T ) : JsonSerializer.Deserialize<T>( sessionData, options );
        }
    }
}
