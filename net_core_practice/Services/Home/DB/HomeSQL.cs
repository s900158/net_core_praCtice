using Dapper;
using net_core_practice.Enums;
using net_core_practice.Services.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Services.Home.DB
{
    public class HomeSQL : IHomeSQL
    {
        private readonly IDBServiceFactory _dBServiceFactory;
        private readonly IDBService _dBService;

        public HomeSQL(IDBServiceFactory dBServiceFactory)
        {
            _dBServiceFactory = dBServiceFactory;
            _dBService = _dBServiceFactory.CreateDBService(DBServiceEnum.JT_DB);
        }

        public async Task<string> GetRedisDataAsync()
        {
            string sql = $@"
              SELECT TOP 1 Name 
                FROM Member  WITH (NOLOCK)
               WHERE ID = @ID
             
                            ";

            var parameter = new DynamicParameters();
            parameter.Add("@ID", 11, DbType.Int32, ParameterDirection.Input);

            string result = await _dBService.QueryFirstOrDefaultAsync<string>(sql, parameter);
            return result;
        }
    }
}
