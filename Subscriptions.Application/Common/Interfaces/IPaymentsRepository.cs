using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IPaymentsRepository
    {
        public Task StorePayment(Payment payment);
    }
}