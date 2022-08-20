using Subscriptions.Domain.Common;
using Subscriptions.Domain.Enums;

namespace Subscriptions.Domain.Entities.Conditions
{
    
    public class NumberOfSubscriptionsCondition : LogicalExpression<int>
    {
        
        public NumberOfSubscriptionsCondition(ComparisonOperator comparisonOperator, int value) : base(comparisonOperator, value)
        {
            
        }
        
        public bool Check(Subscriber subscriber) 
            => base.Check(subscriber.NumberOfSubscriptions);

    }
}