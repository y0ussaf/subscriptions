using System;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Enums;

namespace Subscriptions.Domain.Entities.Conditions
{
    public class JoinDateCondition :  LogicalExpression<DateTime>
    {
        


        public JoinDateCondition(ComparisonOperator comparisonOperator, DateTime value) : base(comparisonOperator, value)
        {
        }
        
        public bool Check(Subscriber subscriber) 
            => base.Check(Value);
    }
}