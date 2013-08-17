using System;
using Core;
using CreditEngine.Events;
using NServiceBus;

namespace StatusGateway
{
    public class CreditCardCreatedEventHandler : IHandleMessages<ICreditCardCreatedEvent>
    {
        public void Handle(ICreditCardCreatedEvent message)
        {
            IApplicantRepository repository = new ApplicantRepositoryFactory().BuildRepository();
            Applicant applicant = repository
                .GetApplicantByCreditCardApplicationId(message.CreditCardApplicationId);
            applicant.CardNumberIssued = message.CardNumber;

            repository.Save(applicant);

            Console.WriteLine("I just handled event {0}", message.CreditCardApplicationId);
        }
    }
}