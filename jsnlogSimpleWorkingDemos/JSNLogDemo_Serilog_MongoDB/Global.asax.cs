using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Serilog;


namespace JSNLogDemo_Serilog_MongoDB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			
			

// -----------------

var log = new LoggerConfiguration()
				.WriteTo.MongoDB("mongodb://localhost/logs", period: TimeSpan.Zero)
				.CreateLogger();

Log.Logger = log;

        }

        protected void Application_BeginRequest()
        {
			
			
        }
    }
}




