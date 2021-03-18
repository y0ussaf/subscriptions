using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IAppsRepository
    {
        public Task RegisterBackendApp(BackendApp app);
        public Task RegisterFrontendApp(FrontendApp app);
    }
}