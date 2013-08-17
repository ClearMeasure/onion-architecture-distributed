using System;
using CreditEngine.Events;
using NServiceBus;

namespace StatusGateway
{
    public class CreditCardCreatedEventHandler : IHandleMessages<ICreditCardCreatedEvent>
    {
        public void Handle(ICreditCardCreatedEvent message)
        {
            Console.WriteLine("I just handled event {0}", message.CreditCardApplicationId);
        }
    }
}