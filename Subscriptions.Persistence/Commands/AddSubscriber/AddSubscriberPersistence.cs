using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Commands.AddSubscriber.Persistence;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Commands.AddSubscriber
{
    public class AddSubscriberPersistence : IAddSubscriberPersistence
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public AddSubscriberPersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task AddSubscriber(string appId, Subscriber subscriber)
        {
            var con = _unitOfWorkContext.GetSqlConnection();
            var sql = @"insert into subscriber (id, app_id, name, email) 
                        values (@id,@appId,@name,@email)";
            await con.ExecuteAsync(sql);
        }
    }
}