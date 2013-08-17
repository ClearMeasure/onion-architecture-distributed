using System;
using NServiceBus;

namespace CreditEngine.Events
{
    public interface ICreditCardApplicationDeclinedEvent : IMessage
    {
        Guid CreditCardApplicationId { get; set; }
        string Reason { get; set; }
    }
}