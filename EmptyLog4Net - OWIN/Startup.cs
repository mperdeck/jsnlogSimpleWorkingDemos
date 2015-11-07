using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Owin;
using JSNLog;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(EmptyLog4Net___OWIN.Startup))]

namespace EmptyLog4Net___OWIN
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseJSNLog();
        }
    }
}

