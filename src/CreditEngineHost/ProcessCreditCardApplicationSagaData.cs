using System;
using NServiceBus.Saga;

namespace CreditEngineHost
{
    public class ProcessCreditCardApplicationSagaData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }

        public virtual long CreditCardApplicationId { get; set; }
        public virtual string ApplicantFirstName { get; set; }
        public virtual string ApplicantLastName { get; set; }
        public virtual DateTime ApplicantDateOfBirth { get; set; }
        public virtual string ApplicantSocialSecurityNumber { get; set; }
    }
}