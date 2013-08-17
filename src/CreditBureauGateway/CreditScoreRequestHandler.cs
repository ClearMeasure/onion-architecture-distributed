using System;
using CreditBureau.Commands;
using NServiceBus;

namespace CreditBureauGateway
{
    public class CreditScoreRequestHandler : IHandleMessages<CreditScoreRequest>
    {
        public IBus Bus { get; set; }

        public void Handle(CreditScoreRequest message)
        {
            Console.WriteLine("I just handled request {0}", message.CreditCardApplicationId);
            
            var response = new CreditScoreResponse(message.CreditCardApplicationId);
            response.CreditScore = 750; //score returned from credit bureau
            response.CreditBureau = message.CreditBureau;
            Bus.Reply(response);
        }
    }
}