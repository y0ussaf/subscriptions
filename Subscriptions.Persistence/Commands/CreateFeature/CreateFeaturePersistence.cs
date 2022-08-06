using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Commands.CreateFeature;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Commands.CreateFeature
{
    public class CreateFeaturePersistence : ICreateFeaturePersistence
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public CreateFeaturePersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public Task<long> CreateFeature(Feature feature)
        {
            var con = _unitOfWorkContext.GetSqlConnection();
            var sql = "insert into feature (name) values (@name) returning id";

            return con.ExecuteScalarAsync<long>(sql, new
            {
                feature.Name
            },_unitOfWorkContext.GetTransaction());
        }
    }
}