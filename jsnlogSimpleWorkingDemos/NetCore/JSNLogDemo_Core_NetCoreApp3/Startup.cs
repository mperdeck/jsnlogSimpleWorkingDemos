using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using JSNLog;

namespace JSNLogDemo_Core_NetCoreApp3
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

			// Configure the server side logging package Serilog to write to a file.
            // JSNLog is not aware what server side logging package you use, so you can use any package you like or none at all.
			//
			// Note that the default level of .Net Core loggers is Information, so Trace and Debug messages are not logged.
            loggerFactory.AddFile("Logs/log.txt");


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




