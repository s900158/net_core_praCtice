using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Models.Options
{
    public class ConnectionStringsModel
    {
        public string EpsonWebDB { get; set; }
        public string NetCoreCacheDB { get; set; }
        public string Redis { get; set; }
    }
}
