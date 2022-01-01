using System.Threading.Tasks;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Repositories
{
    public class InvoicesRepository : IInvoicesRepository
    {
        public Task StoreInvoice(Invoice invoice)
        {
            throw new System.NotImplementedException();
        }
    }
}