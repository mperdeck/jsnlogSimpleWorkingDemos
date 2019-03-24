using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using JSNLog;
using Microsoft.Extensions.Logging;

namespace JSNLogDemo_Core_NetCoreApp2
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

			// Configure the server side logging package Serilog to write to a file.
            // JSNLog is not aware what server side logging package you use, so you can use any package you like or none at all.
			//
			// Note that the default level of .Net Core loggers is Information, so Trace and Debug messages are not logged.
            loggerFactory.AddFile("Logs/log.txt");

			
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Configure JSNLog
			// Do this before calling UseStaticFiles.
			//
            // You can initialize jsnlogConfiguration in code as shown here, or from configuration, such as from an appsettings.json file.
			// If you use the default JsnlogConfiguration object (without setting any properties), you get the default configuration, which will work fine.
			// For a description of the properties, see
			// http://jsnlog.com/Documentation/Configuration/JSNLog
			// http://jsnlog.com/Documentation/Configuration/JSNLog/AjaxAppender
			// http://jsnlog.com/Documentation/Configuration/JSNLog/Logger
			
            var jsnlogConfiguration = new JsnlogConfiguration()
            {
                maxMessages = 50
            };
            app.UseJSNLog(new LoggingAdapter(loggerFactory), jsnlogConfiguration);
			
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}



