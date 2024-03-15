using System.ComponentModel.DataAnnotations;

namespace TinyShop.Models
{
    public class Customer : SoftDeletableEntity
    {
        [Required(ErrorMessage = "Вкажіть ім'я"), StringLength( 256, MinimumLength = 3 ), Display( Name = "Ім'я*" )]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Вкажіть прізвище"), StringLength( 256, MinimumLength = 3 ), Display( Name = "Прізвище*" )]
        public string LastName { get; set; }
        [StringLength( 256, MinimumLength = 3 ), Display( Name = "По-батькові" )]
        public string MiddleName { get; set; }
        [StringLength( 256, MinimumLength = 3 ), Display( Name = "e-mail" )]
        public string Email { get; set; }
        [Required(ErrorMessage = "Вкажіть телефон"), StringLength( 256, MinimumLength = 3 ), Display( Name = "Телефон*" )]
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
