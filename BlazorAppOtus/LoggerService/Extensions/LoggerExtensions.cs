using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder.Internal;
using LoggerService.Middleware;
using Microsoft.AspNetCore.Builder;

namespace LoggerService.Extensions
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Agrega el servicio ILoggerManager como Singleton
        /// </summary>
        /// 
        public static void ConfigureLoggerService(this IServiceCollection services,IConfiguration configuration, ILoggingBuilder loggingBuilder/*, IWebHostEnvironment env*/)
        {
            var configFileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logging", "nlog.config").ToString();
            if (File.Exists(configFileLocation))
            {
                NLog.LogManager.LoadConfiguration(configFileLocation);

            }

            services.AddSingleton(typeof(ILoggerManager<>), typeof(LoggerManager<>));

            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(LogLevel.Trace);
            loggingBuilder.AddConfiguration(configuration.GetSection("Logging")); //appsettings.json
            loggingBuilder.AddConsole(); //Adds a console logger named 'Console' to the factory.
            loggingBuilder.AddDebug(); //Adds a debug logger named 'Debug' to the factory.
            loggingBuilder.AddEventSourceLogger(); //Adds an event logger named 'EventSource' to the fact
            loggingBuilder.AddNLog();
            //loggingBuilder.Configure((hostingContext, logging) =>
            //{
            //    logging.ClearProviders();
            //    logging.SetMinimumLevel(LogLevel.Trace);
            //    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging")); //appsettings.json
            //    logging.AddConsole(); //Adds a console logger named 'Console' to the factory.
            //    logging.AddDebug(); //Adds a debug logger named 'Debug' to the factory.
            //    logging.AddEventSourceLogger(); //Adds an event logger named 'EventSource' to the factory.
            //})
            //    .UseNLog();
        }

        public static void ConfigureRequestLoggingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            //Add our new middleware to the pipeline
            applicationBuilder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }



        public static IDisposable BeginScopeWith(this ILogger logger, params (string key, object value)[] keys)
        {
            return logger.BeginScope(keys.ToDictionary(x => x.key, x => x.value));
        }

   
    }
}
