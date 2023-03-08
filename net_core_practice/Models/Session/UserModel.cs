using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Models.Session
{
    public class UserModel
    {
        public int MemberCno { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
