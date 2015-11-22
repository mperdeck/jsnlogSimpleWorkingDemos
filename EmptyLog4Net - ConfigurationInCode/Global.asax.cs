using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

using JSNLog;

namespace EmptyLog4Net___ConfigurationInCode
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            JavascriptLogging.SetJsnlogConfiguration(new JsnlogConfiguration
            {
                serverSideMessageFormat = "%logger, %level, %message",
                productionLibraryPath = "~/Scripts/jsnlog.min.js",
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

