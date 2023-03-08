using Microsoft.AspNetCore.Http;
using net_core_practice.Models.Session;
using net_core_practice.Utilis.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice.Services.Session.Wappers
{
    public class SessionWapper : ISessionWapper
    {
        private static readonly string _userKey = "session.user";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionWapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session;
            }
        }

        public UserModel User
        {
            get
            {
                return Session.GetObject<UserModel>(_userKey);
            }
            set
            {
                Session.SetObject(_userKey, value);
            }
        }
    }
}
