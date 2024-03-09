using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class Customer : SoftDeletableEntity
    {
        [Required, StringLength( 256, MinimumLength = 3 ), Display( Name = "First Name" )]
        public string FirstName { get; set; }
        [Required, StringLength( 256, MinimumLength = 3 ), Display( Name = "Last Name" )]
        public string LastName { get; set; }
        [StringLength( 256, MinimumLength = 3 ), Display( Name = "Middle Name" )]
        public string MiddleName { get; set; }
        [StringLength( 256, MinimumLength = 3 ), Display( Name = "e-mail" )]
        public string Email { get; set; }
        [Required, StringLength( 256, MinimumLength = 3 ), Display( Name = "Phone" )]
        public string Phone { get; set; }

        public override bool Equals( object obj )
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Customer customer = obj as Customer;
            return FirstName.Trim().ToUpper().Equals( customer.FirstName.Trim().ToUpper() ) 
                && LastName.Trim().ToUpper().Equals( customer.LastName.Trim().ToUpper() ) 
                // I'm not sure if we need to compare MiddleName; customer can input it one time but not in other
                //&& MiddleName.Trim().ToUpper().Equals( customer.MiddleName.Trim().ToUpper() ) 
                && Email.Trim().Equals( customer.Email.Trim() ) 
                && Phone.Trim().Equals( customer.Phone.Trim() );
        }

        public override int GetHashCode()
        {
            return (FirstName + LastName + MiddleName + Email + Phone).GetHashCode();
        }

        public static bool operator ==( Customer a, Customer b )
        {
            return a.Equals( b );
        }
        public static bool operator !=( Customer a, Customer b )
        {
            return !a.Equals( b );
        }
    }
}
