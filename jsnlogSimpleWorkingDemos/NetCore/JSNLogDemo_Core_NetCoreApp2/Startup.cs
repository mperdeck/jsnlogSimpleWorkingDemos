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

			// JSNLog simply passes incoming client side log messages to the standard Net Core logging infrastructure.
            // As a result, those messages wind up in the same logs where your server side log messages are stored. 
			//
			// By default, Net Core sends all log messages to the console. If you run this site by hitting F5 in Visual Studio, you will see
			// those messages in the output window in Visual Studio. This will include the client side log messages sent by JSNLog.

			
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
			// For configuration options, see
			// https://jsnlog.com/Documentation/Configuration
            app.UseJSNLog(loggerFactory, new JsnlogConfiguration());

			
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




