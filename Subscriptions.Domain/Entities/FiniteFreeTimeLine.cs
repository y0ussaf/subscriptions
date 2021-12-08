using System;

namespace Subscriptions.Domain.Entities
{
    public class FiniteFreeTimeLine : FreeTimeLine
    {
        public override bool IsValid(DateTime date)
        {
            return true;
        }
    }
}