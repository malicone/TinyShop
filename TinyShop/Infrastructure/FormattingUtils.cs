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
            var formatted = value.ToString("#,0", nfi); // "1 234 897"
            if (addCurrencyShortcut)
                formatted = "$" + formatted;
            return formatted;
        }
    }
}
