using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Linq;

namespace TinyShop.Infrastructure
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Orders a sequence of strings by their natural numeric order
        /// (e.g. "abc1", "abc2", "abc10" instead of "abc1", "abc10", "abc2").
        /// https://stackoverflow.com/questions/248603/natural-sort-order-in-c-sharp
        /// </summary>
        public static IEnumerable<T> OrderByNatural<T>( this IEnumerable<T> items, Func<T, string> selector, 
            StringComparer stringComparer = null )
        {
            var regex = new Regex( @"\d+", RegexOptions.Compiled );
            int maxDigits = items.SelectMany( 
                i => regex.Matches( selector( i ) ).Cast<Match>().Select( digitChunk => (int?)digitChunk.Value.Length ) )
                .Max() ?? 0;

            return items.OrderBy( 
                i => regex.Replace( 
                    selector( i ), match => match.Value.PadLeft( maxDigits, '0' ) ), 
                        stringComparer ?? StringComparer.CurrentCulture );
        }
    }
}
