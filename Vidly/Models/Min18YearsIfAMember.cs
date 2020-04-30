using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId ==(byte)EnumForMembershipTypes.Unknown || customer.MembershipTypeId ==(byte)EnumForMembershipTypes.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if(customer.DateOfBirth==null)
            {
                return new ValidationResult("Date Of Birth is Required in case of any Membership plan");
            }

            var age = System.DateTime.Now.Year - customer.DateOfBirth.Value.Year;
            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Customer should be atleast 18 years old to get Membership!");
        }
    }
}