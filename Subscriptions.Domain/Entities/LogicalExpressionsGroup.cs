using System.Collections.Generic;
using Subscriptions.Domain.Entities.Conditions;

namespace Subscriptions.Domain.Entities
{
    public abstract class LogicalExpressionsGroup : ConditionToken
    {
        public string Name { get; set; }
        public List<ConditionToken> ConditionTokens { get; set; }
    }
}