using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class InfinitePaidTimeLine : PaidTimeLine
    {
        public InfinitePaidTimeLine(DateTimeRange dateTimeRange) : base(dateTimeRange)
        {
            
        }

        public override bool IsValid(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}