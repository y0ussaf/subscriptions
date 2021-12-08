using System;

namespace Subscriptions.Domain.Entities
{
    public class PaidTimeLine : TimeLine
    {
        public override bool IsValid(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Invoice Invoice { get; set; }
        public double Price { get; set; }
    }
}