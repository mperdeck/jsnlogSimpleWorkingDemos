using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using JSNLog;

using System.Linq;
using System.Collections.Generic;


namespace JSNLogDemo_Core_LoggingEventHandlers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

			// JSNLog simply passes incoming client side log messages to the standard Net Core logging infrastructure.
            // As a result, those messages wind up in the same logs where your server side log messages are stored. 
			//
			// By default, Net Core sends all log messages to the console. If you run this site by hitting F5 in Visual Studio, you will see
			// those messages in the output window in Visual Studio. This will include the client side log messages sent by JSNLog.


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }


            // Configure JSNLog
			// Do this before calling UseStaticFiles.
			//
			// For configuration options, see
			// https://jsnlog.com/Documentation/Configuration
            app.UseJSNLog(loggerFactory);




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


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}






