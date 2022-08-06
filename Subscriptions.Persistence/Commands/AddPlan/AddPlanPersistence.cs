using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Commands.CreatePlan.Persistence;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Commands.AddPlan
{
    public class AddPlanPersistence : IAddPlanCommandPersistence
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public AddPlanPersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }


        public async Task<long> AddPlan(Plan plan)
        {
            var sql = "insert into plan (name,description) values (@name,@description) returning id";
            var con = _unitOfWorkContext.GetSqlConnection();
            return (long) await con.ExecuteScalarAsync(sql, new
            {
                plan.Description,
                plan.Name
            },_unitOfWorkContext.GetTransaction());        }
    }
}