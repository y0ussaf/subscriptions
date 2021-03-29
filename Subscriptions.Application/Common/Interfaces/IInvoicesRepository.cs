using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IInvoicesRepository
    {
        public Task StoreInvoice(Invoice invoice);
        
    }
}