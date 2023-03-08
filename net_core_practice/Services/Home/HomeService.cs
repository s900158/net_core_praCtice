using net_core_practice.Services.Home.DB;
using net_core_practice.Services.Redis;
using net_core_practice.Services.Redis.Enum;
using net_core_practice.Services.Session.Wappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly IHomeSQL _homeSQL;
        private readonly ISessionWapper _sessionWapper;
        private readonly IRedisService _redisService;

        public HomeService(IHomeSQL homeSQL, ISessionWapper sessionWapper, IRedisService redisService)
        {
            _homeSQL = homeSQL;
            _sessionWapper = sessionWapper;
            _redisService = redisService;
        }
        public async Task<string> GetRedisDataAsync()
        {
            // 從快取伺服器取資料
            string redisData = _redisService.GetObject<string>(nameof(RedisKeyEnum.example), (int)RedisDBEnum.靜態資料);

            if (redisData == null)
            {
                redisData = await _homeSQL.GetRedisDataAsync();

                // 寫入快取伺服器
                if (redisData != null)
                {
                    _redisService.SetObject(nameof(RedisKeyEnum.example), redisData, 5, (int)RedisDBEnum.靜態資料);
                }
            }

            return redisData;
        }
    }
}
