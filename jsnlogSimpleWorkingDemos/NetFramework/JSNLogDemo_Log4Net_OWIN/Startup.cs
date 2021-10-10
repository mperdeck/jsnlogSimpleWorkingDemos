using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Owin;
using JSNLog;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(JSNLogDemo_Log4Net_OWIN.Startup))]

namespace JSNLogDemo_Log4Net_OWIN
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseJSNLog();
        }
    }
}



