using System;
using NServiceBus;

namespace CreditEngine.Events
{
    public interface ICreditCardCreatedEvent : IMessage
    {
        long CreditCardApplicationId { get; set; }
        string CardNumber { get; set; }
    }
}