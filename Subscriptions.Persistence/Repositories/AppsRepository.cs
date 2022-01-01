using System;
using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Repositories
{
    public class AppsRepository : IAppsRepository
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public AppsRepository(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task<long> RegisterBackendApp(BackendApp app)
        {
 
            var sql = "insert into app (name,type,secret) values (@name,@type,@secret) returning id;";
            var con = _unitOfWorkContext.GetSqlConnection();
            return await con.ExecuteScalarAsync<long>(sql,new
            {
                app.Name,
                app.Secret,
                Type = app.Type.ToString().ToLower()
            });
            
        }
        

        public async Task<long> RegisterFrontendApp(FrontendApp app)
        {
            var sql = "insert into app (name,type) values (@name,@type) returning id;";
            var con = _unitOfWorkContext.GetSqlConnection();
            return await con.ExecuteScalarAsync<long>(sql,new
            {
                app.Name,
                Type = app.Type.ToString().ToLower()
            },_unitOfWorkContext.GetTransaction());
            
        }

        public async Task<App> GetAppById(long id)
        {
            var sql = "select * from app where id = @id";
            var con = _unitOfWorkContext.GetSqlConnection();
            await using (var reader = await con.ExecuteReaderAsync(sql,id,_unitOfWorkContext.GetTransaction()))
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                var discriminator = Enum.Parse<AppType>(reader.GetString(reader.GetOrdinal(nameof(App.Type))));
                if (discriminator == AppType.Backend)
                {
                    var rowParser = reader.GetRowParser<BackendApp>();
                    return rowParser.Invoke(reader);
                }

                if (discriminator == AppType.Frontend)
                {
                    var rowParser = reader.GetRowParser<FrontendApp>();
                    return rowParser.Invoke(reader);
                }

                throw new Exception();
            }
        }

        public async Task<bool> Exist(long id)
        {
            var sql = "select 1 from app where id = @id";
            var con = _unitOfWorkContext.GetSqlConnection();
            var reader = await con.ExecuteReaderAsync(sql, new {id}, _unitOfWorkContext.GetTransaction());
            return reader.HasRows;
        }

        public async Task SetDefaultPlan(long appId, string planName)
        {
            var sql = "update app set defaultPlanName = @planName where id = @appId";
            var con = _unitOfWorkContext.GetSqlConnection();
            await con.ExecuteAsync(sql, new {appId, planName}, _unitOfWorkContext.GetTransaction());
        }
    }
}