using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Models
{
    // todo: create table Options
    public class GlobalOptions
    {
        public static string SiteTitle { get { return "Магазин Фіалка"; } }
        public static string ContactEmail { get { return "tinyshop@gmail.com"; } }
        public static string ContactPhone { get { return "099 123 45 67"; } }

        public static string ContactsAll
        {
            get
            {
                return $"т. {ContactPhone}, e-mail: {ContactEmail}";
            }
        }

        public static string Payment1 { get { return "картка Приватбанку 1234 5678 1234 5678, Петренко П.П."; } }

        public static string PaymentsAll
        {
            get
            {
                return $"{Payment1}";
            }
        }

        public static string Delivery1 { get { return "Нова Пошта"; } }
        public static string Delivery2 { get { return "самовивіз м. Луцьк (Волинська обл.)"; } }

        public static string DeliveryAll
        {
            get
            {
                return $"{Delivery1}, {Delivery2}";
            }
        }
    }
}
