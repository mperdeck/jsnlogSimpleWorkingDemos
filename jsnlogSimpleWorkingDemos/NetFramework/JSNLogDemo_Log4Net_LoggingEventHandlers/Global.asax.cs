using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using JSNLog;


namespace JSNLogDemo_Log4Net_LoggingEventHandlers
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			
			
            // Add logging handler to JSNLog that:
            // 1) suppresses all messages containing the string "this will be suppressed"
            // 2) adds all request headers to the remaining messages

            // Create logging event handler
            LoggingHandler loggingHandler = (LoggingEventArgs loggingEventArgs) =>
            {
                if (loggingEventArgs.FinalMessage.Contains("this will be suppressed"))
                {
                    // Tell JSNLog not to log this message
                    loggingEventArgs.Cancel = true;
                    return;
                }

                Dictionary<string, string> logRequestHeaders = loggingEventArgs.LogRequest.Headers;
                string logRequestHeadersString =
                    string.Join(" | ", logRequestHeaders.Select(m => m.Key + ":" + m.Value).ToArray());

                // Add string with headers to the log message that will be sent to the logging package
                loggingEventArgs.FinalMessage += " >> Request Headers >> " + logRequestHeadersString;
            };

            // Add the new handler to the logging event, so it will be called when a log message is 
            // about to be sent to the server side logging package.
            JavascriptLogging.OnLogging += loggingHandler;

log4net.Config.XmlConfigurator.Configure();

        }

        protected void Application_BeginRequest()
        {
			
			
        }
    }
}





