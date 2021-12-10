using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class InfiniteFreeTimeLine : FreeTimeLine
    {
        public override bool IsValid(DateTime date)
        {
            throw new NotImplementedException();
        }

        public InfiniteFreeTimeLine(DateTimeRange dateTimeRange) : base(dateTimeRange)
        {
        }
    }
}