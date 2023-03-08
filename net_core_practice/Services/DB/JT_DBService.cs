using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Services.DB
{
    public class JT_DBService : DBService
    {
        private readonly AppSettingsOptions _appSettings;
        public JT_DBService(IOptions<AppSettingsOptions> setting)
        {
            _appSettings = setting.Value;
        }
        protected override IDbConnection CreateConnection()
        {
            return new SqlConnection(_appSettings.ConnectionStrings.EpsonWebDB);
        }
    }
}
