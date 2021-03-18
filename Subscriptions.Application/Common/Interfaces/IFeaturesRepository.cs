using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IFeaturesRepository
    {
        Task AddFeatureToPlan(Feature feature);
    }
}