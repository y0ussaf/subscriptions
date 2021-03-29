using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IPaymentMethodsRepository
    {
        public Task StorePaymentMethod(PaymentMethod paymentMethod);
    }
}