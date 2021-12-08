using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class FiniteFreeTimeLineDescription : TimeLineDescription
    {
        public Expiration Expiration { get; set; }
    }
}