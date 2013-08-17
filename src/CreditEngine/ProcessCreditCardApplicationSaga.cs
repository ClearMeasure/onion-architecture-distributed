using System;
using CreditBureau.Commands;
using CreditEngine.Commands;
using CreditEngine.Events;
using NServiceBus;
using NServiceBus.Saga;

namespace CreditEngine
{
    public class ProcessCreditCardApplicationSaga : Saga<ProcessCreditCardApplicationSagaData>,
        IAmStartedByMessages<ApplyForCreditCommand>,
        IHandleMessages<CreditScoreResponse>
    {
        public void Handle(ApplyForCreditCommand message)
        {
            Console.WriteLine("Handling " + message.GetType().Name);
            MapSagaData(message);

            CreditScoreRequest request = new CreditBureauMessageMapper().CreateRequest(Data);
            request.CreditBureau = CreditBureaus.Experian;
            Bus.Send(request);
            Console.WriteLine("Handling " + message.GetType().Name);
        }

        public void Handle(CreditScoreResponse message)
        {
            LogDebugInfo(message.CreditCardApplicationId, message);

            if (message.CreditScore > 600)
            {
                Bus.Publish<ICreditCardCreatedEvent>(e =>
                {
                    e.CreditCardApplicationId = Data.CreditCardApplicationId;
                    e.CardNumber = "1234-5678-9012-3456"; //simulate process for finding next available number
                });
            }
            else
            {
                Bus.Publish<ICreditCardApplicationDeclinedEvent>(@event =>
                {
                    @event.CreditCardApplicationId = Data.CreditCardApplicationId;
                    @event.Reason = "Insufficient credit score of " + message.CreditScore;
                });
            }

            MarkAsComplete();
        }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<CreditScoreResponse>(data => data.CreditCardApplicationId,
                response => response.CreditCardApplicationId);
        }

        private void MapSagaData(ApplyForCreditCommand message)
        {
            Data.CreditCardApplicationId = message.CreditCardApplicationId;
            Data.ApplicantFirstName = message.ApplicantFirstName;
            Data.ApplicantLastName = message.ApplicantLastName;
            Data.ApplicantDateOfBirth = message.ApplicantDateOfBirth;
            Data.ApplicantSocialSecurityNumber = message.ApplicantSocialSecurityNumber;
        }

        private static void LogDebugInfo(Guid id, IMessage message)
        {
            Console.WriteLine("Processing response {0}-{1}", id, message.GetType().Name);
        }
    }
}