using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Serilog;
using System.IO;


namespace JSNLogDemo_Serilog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			
			

// -----------------

string logFilePath = Server.MapPath("/Logs/log.txt");

var log = new LoggerConfiguration()
				.WriteTo.File(logFilePath, buffered: false)
				.MinimumLevel.Verbose()
				.CreateLogger();

Log.Logger = log;

        }

        protected void Application_BeginRequest()
        {
			
			
        }
    }
}




