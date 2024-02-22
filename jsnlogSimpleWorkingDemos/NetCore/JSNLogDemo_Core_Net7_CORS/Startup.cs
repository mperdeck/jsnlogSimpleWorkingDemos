using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using JSNLog;


namespace JSNLogDemo_Core_Net7_CORS
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
            app.UseJSNLog(loggerFactory, new JsnlogConfiguration()
            {
                insertJsnlogInHtmlResponses = true,
                productionLibraryPath = "https://cdnjs.cloudflare.com/ajax/libs/jsnlog/2.30.0/jsnlog.min.js",
                defaultAjaxUrl = "http://apicorslocalhost.local/jsnlog.logger",
  
                // Allow requests from localhost (the host used when hitting F5 in Visual Studio)
                corsAllowedOriginsRegex = @"^https?:\/\/([a-z0-9]+[.])*localhost.*",

                // Allow custom headers X-MyHeader and X-MyHeader2
                corsAllowedHeaders = "X-MyHeader, X-MyHeader2"
            });




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




