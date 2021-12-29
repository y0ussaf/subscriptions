using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IAppsRepository
    {
        public Task<long> RegisterBackendApp(BackendApp app);
        public Task<long> RegisterFrontendApp(FrontendApp app);
        public Task<App> GetAppById(long id);
        public Task<bool> Exist(long id);
        Task SetDefaultPlan(long appId, string planName);
    }
}