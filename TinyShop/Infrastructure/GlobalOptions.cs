using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Infrastructure
{
    // todo: create table Options
    public class GlobalOptions
    {
        public static string SiteTitle { get { return "Пролісок"; } }
        public static string CatalogTitle { get { return "Каталог"; } }
        public static string ContactEmail { get { return "tinyshop@gmail.com"; } }
        public static string ContactPhone { get { return "099 123 45 67"; } }

        public static string ContactsAll
        {
            get
            {
                return $"т. {ContactPhone}, e-mail: {ContactEmail}";
            }
        }

        public static string Payment1 { get { return "card number 1234 5678 1234 5678, John Smith"; } }

        public static string PaymentsAll
        {
            get
            {
                return $"{Payment1}";
            }
        }

        public static string Delivery1 { get { return "NewId Post"; } }
        public static string Delivery2 { get { return "pickup Lutsk (Volyn region)"; } }

        public static string DeliveryAll
        {
            get
            {
                return $"{Delivery1}, {Delivery2}";
            }
        }
    }
}
