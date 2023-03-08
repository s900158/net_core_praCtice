using Microsoft.Extensions.Options;
using net_core_practice.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Services.DB
{
    public class DBServiceFactory : IDBServiceFactory
    {
        IEnumerable<IDBService> _dbServices;

        public DBServiceFactory(IOptions<AppSettingsOptions> settings, IEnumerable<IDBService> dbServices)
        {
            _dbServices = dbServices;
        }

        private readonly Dictionary<DBServiceEnum, Type> _dictDBServiceFactory = new Dictionary<DBServiceEnum, Type>
        {
            { DBServiceEnum.JT_DB, typeof(JT_DBService) },
        };

        public IDBService CreateDBService(DBServiceEnum connectionEnum)
        {
            if (_dictDBServiceFactory.TryGetValue(connectionEnum, out Type ConnectionType))
            {
                IDBService connectionObj = _dbServices.Where(c => c.GetType().Name == ConnectionType.Name).SingleOrDefault();
                return (IDBService)connectionObj;
            }
            else
            {
                throw new Exception("不支援此類型");
            }
        }
    }
}
