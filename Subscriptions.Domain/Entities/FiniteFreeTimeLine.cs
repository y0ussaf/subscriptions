using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class FiniteFreeTimeLine : FreeTimeLine
    {
        public override bool IsValid(DateTime date)
        {
            return true;
        }

        public FiniteFreeTimeLine(DateTimeRange dateTimeRange) : base(dateTimeRange)
        {
        }
    }
}