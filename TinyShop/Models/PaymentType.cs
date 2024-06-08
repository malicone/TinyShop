using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyShop.Models
{
    public class PaymentType : NamedEntity
    {
        // Values from the db
        [NotMapped]
        public static int CardId { get { return 1; } }

        [NotMapped]
        public static int PostpaidId { get { return 2; } }
    }
}
