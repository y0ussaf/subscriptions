using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IAppsRepository
    {
        public Task RegisterApp(App app);
    }
}