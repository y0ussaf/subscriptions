using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class PaidSubscription : ExpiredSubscription
    {
        
        public bool Paid { get; set; }

        public override bool IsValid()
        {
            return base.IsValid()  && Paid ;
        }

    }
}