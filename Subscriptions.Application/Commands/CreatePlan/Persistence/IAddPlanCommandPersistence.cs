using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreatePlan.Persistence
{
    public interface IAddPlanCommandPersistence
    {
        Task<long> AddPlan(Plan plan);
    }
}