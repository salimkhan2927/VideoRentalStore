using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
       
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(255)]
        public string Name { get; set; }
       
        public bool IsSubscribedToNewsletter { get; set; }
       
        public MembershipType MembershipType { get; set; }
        
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        [Min18YearsIfAMember]
        [Display(Name="Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

    }
    enum EnumForMembershipTypes
    {
        Unknown=0,
        PayAsYouGo=1,
        Monthly=2,
        Quarterly=3,
        Yearly=4
    }
}