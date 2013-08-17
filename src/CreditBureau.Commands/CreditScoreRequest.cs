using System;
using NServiceBus;

namespace CreditBureau.Commands
{
    public class CreditScoreRequest : IMessage
    {
        public CreditScoreRequest(long requestId)
        {
            CreditCardApplicationId = requestId;
        }

        public long CreditCardApplicationId { get; set; }
        public string ApplicantFirstName { get; set; }
        public string ApplicantLastName { get; set; }
        public DateTime ApplicantDateOfBirth { get; set; }
        public string ApplicantSocialSecurityNumber { get; set; }
        public CreditBureaus CreditBureau { get; set; }
    }
}