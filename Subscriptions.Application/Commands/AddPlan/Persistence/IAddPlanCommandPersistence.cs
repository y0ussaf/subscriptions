using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddPlan.Persistence
{
    public interface IAddPlanCommandPersistence
    {
        Task AddPlan(Plan plan);
    }
}