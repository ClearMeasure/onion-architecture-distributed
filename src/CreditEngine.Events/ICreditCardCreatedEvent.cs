using System;
using NServiceBus;

namespace CreditEngine.Events
{
    public interface ICreditCardCreatedEvent : IMessage
    {
        Guid CreditCardApplicationId { get; set; }
        string CardNumber { get; set; }
    }
}