namespace Subscriptions.Application.Common.Interfaces
{
    public interface ILogicalExpressionEvaluator
    {
        bool Evaluate(string expression);
    }
}