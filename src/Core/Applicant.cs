using System;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Applicant
    {
        public Guid CreditCardApplicationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SocialSecurityNumber { get; set; }

        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        public string PathAndQuerystring { get; set; }
        public string LoginName { get; set; }
        public DateTime VisitDate { get; set; }
        public string IpAddress { get; set; }

        [DataType(DataType.MultilineText)]
        public string Browser { get; set; }

        public string CardNumberIssued { get; set; }
    }
}