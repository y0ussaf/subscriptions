using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Queries.Offers.GetIntervalsDefinitions;
using Subscriptions.Application.Queries.Offers.GetIntervalsDefinitions.Persistence;

namespace Subscriptions.Persistence.Queries.Offers.GetTimelinesDefinitions
{
    public class GetIntervalsDefinitionsPersistence : IGetTimelinesDefinitionsPersistence
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public GetIntervalsDefinitionsPersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task<List<IntervalDefinitionDto>> GetIntervalsDefinitions(long offerId)
        {
            var sql = @"select * from timeline_definition where offer_id = @offerId";
            var con = _unitOfWorkContext.GetSqlConnection();
            return (await con.QueryAsync<IntervalDefinitionDto>(sql, new
            {
                offerId
            })).ToList();
        }
    }
}