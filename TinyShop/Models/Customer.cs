﻿using System.ComponentModel.DataAnnotations;

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
            Customer target = obj as Customer;
            return FirstName.Trim().ToUpper().Equals( target.FirstName.Trim().ToUpper() ) 
                && LastName.Trim().ToUpper().Equals( target.LastName.Trim().ToUpper() ) 
                // I'm not sure if we need to compare MiddleName; buyer can input it one time but not in other
                //&& MiddleName.Trim().ToUpper().Equals( target.MiddleName.Trim().ToUpper() ) 
                && Email.Trim().Equals( target.Email.Trim() ) 
                && Phone.Trim().Equals( target.Phone.Trim() );
        }

        public override int GetHashCode()
        {
            return (FirstName + LastName + MiddleName + Email + Phone).GetHashCode();
        }

        public static bool operator ==( Customer a, Customer b )
        {
            if ( ( a is null ) && ( b is null ) )
            {
                return true;
            }
            if ( a is null )
            {
                return false;
            }
            return a.Equals( b );
        }
        public static bool operator !=( Customer a, Customer b )
        {
            if ( ( a is null ) && ( b is null ) )
            {
                return false;
            }
            if ( a is null )
            {
                return true;
            }
            return !a.Equals( b );
        }
    }
}
