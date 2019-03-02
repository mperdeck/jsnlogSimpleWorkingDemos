using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace JSNLogDemo_Core_NetCoreRequestId
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NLog: setup the logger first to catch all errors
            var currentDir = Directory.GetCurrentDirectory();
            var logger = NLogBuilder
                .ConfigureNLog($@"{currentDir}\{Startup.ConfigurationFolder}\NLog.Internal.config")
                .GetCurrentClassLogger();

            // Allows for <target name="file" xsi:type="File" fileName = "${var:logDirectory}logfile.log"... >
            NLog.LogManager.Configuration.Variables["logDirectory"] = currentDir + "\\";

            try
            {
                logger.Trace($"Configuration of {nameof(WebHost)} starting.");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                logger.Fatal(e, $"Application stopped after Exception. {e.Message}");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // set NLog config, so it can be used by .UseNLog()
                    var env = hostingContext.HostingEnvironment;
                    env.ConfigureNLog($@"{Startup.ConfigurationFolder}\NLog.Default.config");
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    // Note: This logging configuration overrides any call to SetMinimumLevel!
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                })
                .UseNLog(NLogAspNetCoreOptions.Default); // NLog: setup NLog for Dependency injection
    }
}


