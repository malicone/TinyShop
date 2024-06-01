using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyShop.Models;

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
                formatted += " грн.";
            return formatted;
        }

        public static string FormatWholesalePrice(ProductPrice price)
        {
            string unitLabelSingle = price.TheProduct.UnitTypeId == ProductUnitType.PairId ? "пару" : "шт.";
            string unitLabelPlural = price.TheProduct.UnitTypeId == ProductUnitType.PairId ? "пар" : "шт.";
            var sbuilder = new StringBuilder();
            sbuilder.Append($"{FormatPrice(price.PricePerUnit)} за 1 {unitLabelSingle},");
            sbuilder.Append($" {FormatPrice(price.PackPrice)} упаковка ({price.UnitsPerPack} {unitLabelPlural}),");
            sbuilder.Append($" мінімальна кількість {price.MinPackSaleQuantity} " +
                            $"{GetPackSingleOrPlural(price.MinPackSaleQuantity)} ({FormatPrice(price.TotalPrice)})");
            return sbuilder.ToString();
        }
        public static string FormatUnitPrice(Product product)
        {
            string unitLabel = product.UnitTypeId == ProductUnitType.PairId ? "пару" : "шт.";
            return FormatPrice(product.PricePerUnit) + " за 1 " + unitLabel;
        }

        public static string FormatPackPrice(Product product)
        {
            string unitLabel = product.UnitTypeId == ProductUnitType.PairId ? "пар" : "шт.";
            return FormatPrice(product.PackPrice) + " упаковка (" + product.UnitsPerPack + " " + unitLabel + ")";
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

        public static string GetPackSingleOrPlural(int count)
        {
            if ( ( count >= 11 ) && ( count <= 19 ) )
                return "упаковок";

            var lastDigit = count % 10;
            if ( lastDigit == 1 )
                return "упаковка";
            if ( lastDigit > 1 && lastDigit < 5 )
                return "упаковки";
            return "упаковок";
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
