using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Models
{
#nullable enable
    public class OrderStatus : NamedEntity
    {
        public OrderStatus() 
        {
            
        }
        /// <summary>
        /// Important! These are values from database.
        /// </summary>
        [NotMapped]
        public static int NewId { get { return 1; } }
        [NotMapped]
        public static int InProgressId { get { return 2; } }
        [NotMapped]
        public static int CompletedId { get { return 3; } }
        [NotMapped]
        public static int CanceledId { get { return 4; } }
    }
#nullable disable
}
