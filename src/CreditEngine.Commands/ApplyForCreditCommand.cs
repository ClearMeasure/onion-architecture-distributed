using System;
using NServiceBus;

namespace CreditEngine.Commands
{
    public class ApplyForCreditCommand : IMessage
    {
        public ApplyForCreditCommand(long id)
        {
            CreditCardApplicationId = id;
        }

        public long CreditCardApplicationId { get; set; }
        public string ApplicantFirstName { get; set; }
        public string ApplicantLastName { get; set; }
        public DateTime ApplicantDateOfBirth { get; set; }
        public string ApplicantSocialSecurityNumber { get; set; }
    }
}