using System;

namespace Subscriptions.Domain.Entities
{
    public class InfiniteFreeTimeLine : FreeTimeLine
    {
        public override bool IsValid(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}