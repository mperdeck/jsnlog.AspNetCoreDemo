using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using JSNLog;

namespace Website
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                   .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                // builder.AddUserSecrets(); 
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
          //  services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Configure JSNLog
            var jsnlogConfiguration = new JsnlogConfiguration(); // See jsnlog.com/Documentation/Configuration

            //  throw new NotImplementedException();
            app.UseJSNLog(new LoggingAdapter(loggerFactory), jsnlogConfiguration);

            app.UseStaticFiles();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Create a catch-all response
            app.Run(async (context) =>
            {
                try
                {
                    var logger = loggerFactory.CreateLogger("Catchall Endpoint");
                    logger.LogInformation("No endpoint found for request {path}", context.Request.Path);
                   // await context.Response.WriteAsync("No endpoint found - try /api/todo.");
                }
                catch (Exception ex)
                {

                }
            });
        }


        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
             .UseKestrel()
            .UseIISIntegration()
            .UseStartup<Startup>()
            .Build();
            host.Run();
        }


    }
}
