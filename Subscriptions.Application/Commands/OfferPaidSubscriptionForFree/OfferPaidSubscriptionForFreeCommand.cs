
using MediatR;
using Subscriptions.Domain.Common;

namespace Subscriptions.Application.Commands.OfferPaidSubscriptionForFree
{
    public class OfferPaidSubscriptionForFreeCommand : IRequest<OfferPaidSubscriptionForFreeCommandResponse>
    {
        public string SubscriberId { get; set; }
        public string OfferId { get; set; }
        public bool CreatingNextPaidCycleAutomatically { get; set; } = true;
        public bool Unlimited { get; set; } = false;
        public string ExpireAfterTimeIn { get; set; } = TimeIn.Months.ToString();
        public int ExpireAfter { get; set; }
    }
}