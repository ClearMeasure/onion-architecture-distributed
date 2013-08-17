using System;
using Core;
using CreditEngine.Events;
using NServiceBus;

namespace StatusGateway
{
    public class CreditCardApplicationDeclinedEventHandler : IHandleMessages<ICreditCardApplicationDeclinedEvent>
    {
        public void Handle(ICreditCardApplicationDeclinedEvent message)
        {
            Console.WriteLine("I just handled event {0}", message.CreditCardApplicationId);
        }
    }
}