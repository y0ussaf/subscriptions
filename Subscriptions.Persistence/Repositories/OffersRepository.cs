using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Repositories
{
    public class OffersRepository : IOffersRepository
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public OffersRepository(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public Task AddOfferToPlan(long appId, string planName, Offer offer)
        {
            var sql = "insert into offer (\"appId\", \"planName\", name, description) values (@appId,@planName,@name,@description);";
            if (offer.TimeLineDefinitions.Any())
            {
            }
            return Task.CompletedTask;
            
        }

        public Task<Offer> GetOfferByName(long appId, string planName, string offerName)
        {
            throw new System.NotImplementedException();
        }

        public Task SetDefaultOfferForPlan(string planId, string offerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Offer> GetOfferByNameIncludingTimelinesDefinitions(long appId, string planName, string offerName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Exist(long appId, string planName, string offerName)
        {
            var sql = "select 1 from offer where \"appId\" = @appId and name = @planName and name = @offerName";
            var con = _unitOfWorkContext.GetSqlConnection();
            await using var reader = await con.ExecuteReaderAsync(sql, new {AppId = appId,PlanName = planName,OfferName = offerName}, _unitOfWorkContext.GetTransaction());
            return reader.HasRows;
        }
    }
}