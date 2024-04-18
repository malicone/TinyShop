using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TinyShop.Infrastructure
{
    public static class FormattingUtils
    {
        public static string FormatPrice(decimal value, bool addCurrencyShortcut = true)
        {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            var formatted = value.ToString("#,0", nfi);
            if (addCurrencyShortcut)
                formatted = formatted + " грн.";
            return formatted;
        }

        public static string GetProductSingleOrPlural(int count)
        {
            if ( ( count >= 11 ) && ( count <= 19 ) )
                return "товарів";

            var lastDigit = count % 10;
            if ( lastDigit == 1 )
                return "товар";
            if ( lastDigit > 1 && lastDigit < 5 )
                return "товара";
            return "товарів";
        }

        public static string FormatDateTime( DateTime value, bool includeTime = true )
        {
            if ( includeTime )
                return value.ToString( "yyyy-MM-dd HH:mm" );
            else
                return value.ToString( "yyyy-MM-dd" );
        }
    }
}
