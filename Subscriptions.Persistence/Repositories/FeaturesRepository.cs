using System.Threading.Tasks;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Repositories
{
    public class FeaturesRepository : IFeaturesRepository
    {
        public Task AddFeatureToPlan(Feature feature)
        {
            throw new System.NotImplementedException();
        }
    }
}