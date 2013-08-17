using System;
using NServiceBus;

namespace CreditBureau.Commands
{
    public class CreditScoreResponse : IMessage
    {
        public Guid CreditCardApplicationId { get; private set; }
        public ushort CreditScore { get; set; }
        public CreditBureaus CreditBureau { get; set; }

        public CreditScoreResponse(Guid applicationId)
        {
            CreditCardApplicationId = applicationId;
        }
    }
}