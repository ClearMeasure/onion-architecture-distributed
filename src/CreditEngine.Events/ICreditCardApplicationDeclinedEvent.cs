using NServiceBus;

namespace CreditEngine.Events
{
    public interface ICreditCardApplicationDeclinedEvent : IMessage
    {
        long CreditCardApplicationId { get; set; }
        string Reason { get; set; }
    }
}