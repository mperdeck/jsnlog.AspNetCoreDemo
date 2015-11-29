using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Facebook;
using Microsoft.AspNet.Authentication.Google;
using Microsoft.AspNet.Authentication.MicrosoftAccount;
using Microsoft.AspNet.Authentication.Twitter;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Routing;
using Microsoft.Data.Entity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.Runtime;
//using Microsoft.Framework.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Framework.Logging.Console;

using Serilog;
using System.IO;



using Serilog.Sinks.IOFile;
using Serilog.Formatting.Raw;
using JSNLog;

namespace WebSite
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.

            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            // Configure Serilog

            string logFilePath = new Constants(appEnv).LogFilePath;

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
                .WriteTo.Sink(new FileSink(logFilePath, new RawFormatter(), null))
               .CreateLogger();

            // Configure JSNLog

            JavascriptLogging.SetJsnlogConfiguration(new JsnlogConfiguration
            {
                loggers = new List<Logger> {
                    new Logger {
                        level="ERROR"
                    }
                }
            });


            //##############################
            //// Configure JSNLog

            //var options = ConfigurationBinder.Bind<AppSettings>(Configuration);


            //JsnlogConfiguration jsnlogConfiguration = Configuration.GetConfigurationSection.Get<JsnlogConfiguration>(("JSNLog");
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC services to the services container.
            services.AddMvc();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton<Constants>();

            // Allow usage of JSNLog in OWIN pipeline
        services.AddSingleton<JsnlogMiddlewareComponent>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddSerilog();
            loggerFactory.AddDebug();

            // Configure the HTTP request pipeline.

            // Add the following to the request pipeline only in development environment.
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // sends the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }

            // Add static files to the request pipeline.
            app.UseStaticFiles();



      //      app.UseOwin(appBuilder => appBuilder.UseJSNLog());
            app.UseMiddleware<JsnlogMiddlewareComponent>();


            // Add JSNLog to the pipeline.
            //  ##############      app.UseJSNLog();



            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });

            // Configure JSNLog
            //
            // Add logging handler, that
            // 1) logs the message using the ASP.NET5 framework;
            // 2) cancels further logging inside JSNLog. 

            LoggingHandler loggingHandler =
                (LoggingEventArgs loggingEventArgs) =>
                {
                    Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger(loggingEventArgs.FinalLogger);

                    Object message;
                    try
                    {
                        message = Newtonsoft.Json.JsonConvert.DeserializeObject<Object>(loggingEventArgs.FinalMessage);
                    }
                    catch
                    {
                        message = loggingEventArgs.FinalMessage;
                    }

                    var requestId = loggingEventArgs.LogRequest.RequestId;
                    var utcDate = loggingEventArgs.LogRequest.UtcDate;

                    switch (loggingEventArgs.FinalLevel)
                    {
                        case Level.TRACE: logger.LogDebug("{logRequestId} - {logTime} - {message}", requestId, utcDate, message); break;
                        case Level.DEBUG: logger.LogVerbose("{logRequestId} - {logTime} - {message}", requestId, utcDate, message); break;
                        case Level.INFO: logger.LogInformation("{logRequestId} - {logTime} - {message}", requestId, utcDate, message); break;
                        case Level.WARN: logger.LogWarning("{logRequestId} - {logTime} - {message}", requestId, utcDate, message); break;
                        case Level.ERROR: logger.LogError("{logRequestId} - {logTime} - {message}", requestId, utcDate, message); break;
                        case Level.FATAL: logger.LogCritical("{logRequestId} - {logTime} - {message}", requestId, utcDate, message); break;
                    }
                };

            JavascriptLogging.OnLogging += loggingHandler;



            //--------------------
            //#######################
            Microsoft.Extensions.Logging.ILogger _logger = loggerFactory.CreateLogger("TestLogger");

            _logger.LogInformation("test page opened at {requestTime}", DateTime.Now);

            var obj = new { i = 5, j = 6 };

            string json = @"{
  'Name': 'Bad Boys',
  'ReleaseDate': '1995-4-7T00:00:00',
  'Genres': ['Action', 'Comedy'      ]
}";

            Object m;
            try
            {
                m = Newtonsoft.Json.JsonConvert.DeserializeObject<Object>(json);

            }
catch
            {
                m = json;
            }
        //    string name = m.Name;
        // Bad Boys




            _logger.LogInformation("test page opened at {message}", obj);

            _logger.LogInformation("test page opened at {message}", m);


        }
    }
}
