using System.Threading.Tasks;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        public Task StorePayment(Payment payment)
        {
            throw new System.NotImplementedException();
        }
    }
}