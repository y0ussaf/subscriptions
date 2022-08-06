using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreateFeature
{
    public interface ICreateFeaturePersistence
    {
        Task<long> CreateFeature(Feature feature);
    }
}