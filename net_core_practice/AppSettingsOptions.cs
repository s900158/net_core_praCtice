using net_core_practice.Models.Options;
using net_core_practice.Models.Options.ApiRouter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_practice
{
    public class AppSettingsOptions
    {
        public ConnectionStringsModel ConnectionStrings { get; set; }

        public ApiRouterModel ApiRouter { get; set; }
    }
}
