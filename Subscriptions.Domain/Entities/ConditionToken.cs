using Subscriptions.Domain.Enums;

namespace Subscriptions.Domain.Entities.Conditions
{
    public abstract class ConditionToken
    {
        public ConditionTokenType TokenType { get; set; }
        
    }
}