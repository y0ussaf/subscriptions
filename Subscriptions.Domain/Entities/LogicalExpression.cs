using System;
using Subscriptions.Domain.Entities.Conditions;
using Subscriptions.Domain.Enums;
using static Subscriptions.Domain.Enums.ComparisonOperator;

namespace Subscriptions.Domain.Entities
{
    public abstract class  LogicalExpression<T> : ConditionToken where T : IComparable<T> 
    {
        protected LogicalExpression(ComparisonOperator comparisonOperator, T value)
        {
            ComparisonOperator = comparisonOperator;
            Value = value;
        }

        private ComparisonOperator ComparisonOperator { get; set; }
        protected T Value { get; set; }
        protected bool Check(T targetValue)
        {
            var res = targetValue.CompareTo(Value);
            return ComparisonOperator switch
            {
                Eq =>  res is 0,
                Ne => res is not 0,
                Gt => res is  1,
                Ge => res is 0 or 1 ,
                Lt => res is -1,
                Le => res is -1 or 0,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}