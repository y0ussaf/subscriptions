using System.Threading.Tasks;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Repositories
{
    public class PaymentMethodsRepository : IPaymentMethodsRepository
    {
        public Task StorePaymentMethod(PaymentMethod paymentMethod)
        {
            throw new System.NotImplementedException();
        }
    }
}