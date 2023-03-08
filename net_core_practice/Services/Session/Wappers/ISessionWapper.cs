using net_core_practice.Models.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Services.Session.Wappers
{
    public interface ISessionWapper
    {
        UserModel User { get; set; }
    }
}
