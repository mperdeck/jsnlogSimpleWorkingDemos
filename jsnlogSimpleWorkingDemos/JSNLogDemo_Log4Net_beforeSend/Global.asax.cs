using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using JSNLog;


namespace JSNLogDemo_Log4Net_beforeSend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			
			
log4net.Config.XmlConfigurator.Configure();

        }

        protected void Application_BeginRequest()
        {
			

			// Use configuration in code instead of configuration in web.config/.

            JavascriptLogging.SetJsnlogConfiguration(new JsnlogConfiguration
            {
                serverSideMessageFormat = "%userHostAddress, %logger, %level, %message",
                productionLibraryPath = "~/Scripts/jsnlog.js",
                loggers = new List<Logger>
                {
                    new Logger 
                    {
                        name = "jsLogger",
                        level = "FATAL"
                    }
                }
            });

			
        }
    }
}





