using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Subscriptions.Application.Commands.AddFeatureToPlan.Persistence;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Commands.AddFeatureToPlan
{
    public class AddFeatureToPlanPersistence : IAddFeatureToPlanCommandPersistence 
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public AddFeatureToPlanPersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task AddFeatureToPlan(PlanFeature planFeature)
        {
            var con = _unitOfWorkContext.GetSqlConnection();
            var sql = @"insert into plan_feature (plan_id, feature_id, details) values (@planId, @featureId, @details)";
            await con.ExecuteAsync(sql, new
            {
                PlanId = planFeature.Plan.Id,
                FeatureId = planFeature.Feature.Id,
                planFeature.Details
            });
        }

        public async Task<bool> PlanExist(string planName)
        {
            var sql = "select 1 from plan where name = @planName";
            var con = _unitOfWorkContext.GetSqlConnection();
            await using var reader = await con.ExecuteReaderAsync(sql, new {PlanName = planName}, _unitOfWorkContext.GetTransaction());
            return reader.HasRows;
            
        }
    }
}