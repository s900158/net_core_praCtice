using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Services.Redis
{
    public class RedisService : IRedisService
    {
        private readonly AppSettingsOptions _appSettings;
        private ConnectionMultiplexer conn;

        public RedisService(IServiceProvider provider)
        {
            _appSettings = (provider.GetService(typeof(IOptions<AppSettingsOptions>)) as IOptions<AppSettingsOptions>).Value;
            conn = ConnectionMultiplexer.Connect(_appSettings.ConnectionStrings.Redis);
        }

        public string Get(string key, int useDb = 2)
        {
            var db = conn.GetDatabase(useDb);
            string rtn = "";
            try
            {
                rtn = db.StringGet(key);
            }
            catch (Exception e)
            {
                Console.WriteLine("RedisService.cs => Get => fail:" + e.Message);
            }

            return rtn;
        }

        public string Get_test(string key, int useDb = 2)
        {
            var db = conn.GetDatabase(useDb);
            string rtn = "";
            try
            {
                rtn = db.StringGet(key);
            }
            catch (Exception e)
            {
                rtn = e.Message;
            }

            return rtn;
        }

        public T GetObject<T>(string key, int useDb = 2)
        {
            T rtn = default(T);
            var db = conn.GetDatabase(useDb);
            try
            {
                string result = db.StringGet(key);
                rtn = JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("RedisService.cs => GetObject => fail:" + e.Message);
            }

            return rtn;
        }

        public bool Remove(string key, int useDb = 2)
        {
            bool rtn = false;
            var db = conn.GetDatabase(useDb);
            try
            {
                rtn = db.KeyDelete(key);
            }
            catch (Exception e)
            {
                Console.WriteLine("RedisService.cs => Remove => fail:" + e.Message);
            }
            return rtn;
        }

        public void Set(string key, string value, int expiresAtMinutes = 0, int useDb = 2)
        {
            var db = conn.GetDatabase(useDb);

            try
            {
                if (expiresAtMinutes == 0)
                {
                    db.StringSet(key, value);
                }
                else
                {
                    db.StringSet(key, value, TimeSpan.FromMinutes(expiresAtMinutes));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("RedisService.cs => Set => fail:" + e.Message);
            }
        }

        public void SetObject(string key, object value, int expiresAtMinutes = 0, int useDb = 2)
        {
            var db = conn.GetDatabase(useDb);
            try
            {
                //物件轉成JSON
                string jsonString = JsonConvert.SerializeObject(value);

                if (expiresAtMinutes == 0)
                {
                    db.StringSet(key, jsonString);
                }
                else
                {
                    db.StringSet(key, jsonString, TimeSpan.FromMinutes(expiresAtMinutes));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("RedisService.cs => SetObject => fail:" + e.Message);
            }
        }

        public string GetRedisConnString()
        {
            string connstring = _appSettings.ConnectionStrings.Redis;
            return connstring;
        }
    }
}
