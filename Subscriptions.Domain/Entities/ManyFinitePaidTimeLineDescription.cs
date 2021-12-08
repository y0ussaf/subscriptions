using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class ManyFinitePaidTimeLineDescription : PaidTimeLineDescription
    {
        public int Repeat { get; set; }
        public Expiration Expiration { get; set; }
    }
}