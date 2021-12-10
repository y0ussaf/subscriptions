using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class FinitePaidTimeLine : PaidTimeLine
    {
        public FinitePaidTimeLine(DateTimeRange dateTimeRange) : base(dateTimeRange)
        {
        }

        public override bool IsValid(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}