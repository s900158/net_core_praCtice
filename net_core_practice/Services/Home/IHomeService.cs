using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Services.Home
{
    public interface IHomeService
    {
        Task<string> GetRedisDataAsync();
    }
}
