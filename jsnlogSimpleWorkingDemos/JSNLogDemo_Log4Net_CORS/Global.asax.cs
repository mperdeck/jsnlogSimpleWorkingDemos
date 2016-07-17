using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using JSNLog;


namespace JSNLogDemo_Log4Net_CORS
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
			
			// Configure CORS.

            JavascriptLogging.SetJsnlogConfiguration(new JsnlogConfiguration
            {
				defaultAjaxUrl = "http://apicorslocalhost.local/jsnlog.logger",
				corsAllowedOriginsRegex = @"^https?:\/\/([a-z0-9]+[.])*corslocalhost[.]local$",
                productionLibraryPath = "~/Scripts/jsnlog.min.js"
            });
			
        }
    }
}





