using System.Data;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Common;

namespace Subscriptions.Application.Common.Services
{
    public class DataTableLogicalExpressionEvaluator : ILogicalExpressionEvaluator
    {
        public bool Evaluate(string expression)
        {
            var table = new DataTable();
            return (bool)table.Compute(expression, "");
        }
    }
}