using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using JSNLog;
using Microsoft.Extensions.Logging;

namespace JSNLogDemo_Core_NetCoreRequestId
{
    public class Startup
    {
        public const string ConfigurationFolder = "Configuration";

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


