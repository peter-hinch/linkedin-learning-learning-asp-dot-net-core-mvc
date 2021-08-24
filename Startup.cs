using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAspDotNetCoreMVC
{
    public class Startup
    {
        // This allows us to access our own custom configuration settings
        // within the class.
        private readonly IConfiguration configuration;

        // A constructor for the Startup object. .NET will populate this with
        // its own confiuration object when the application starts.
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Use the exception handler to serve a custom error page
            // (providing that the environment variable ASPNETCORE_ENVIRONMENT
            // is set to "Development").
            app.UseExceptionHandler("/error.html");

            // This determines whther the environment variable is set.
            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            */

            // Alternatively we can use our own environment variable in
            // conjunction with the configuration in the Startup object.
            // The configuration API can navigate complex objects in the
            // appsettings.json file (children are accesses using a : ).

            // The configuration API will first look for the environment
            // variable ASPNETCORE_ENVIRONMENT being set to "Development".
            // If this is the case, the appsettings.Development.json file
            // is read. If not, it uses appsettings.json for settings.
            if (configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions")) 
            {
                app.UseDeveloperExceptionPage();
            }

            /*
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
            */

            /*
            app.Use(async (context, next) =>
            {
                if(context.Request.Path.Value.StartsWith("/hello"))
                {
                    await context.Response.WriteAsync("Hello ASP.NET Core!");
                }
                await next();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("How are you?");
            });
            */

            // This code tests the error handling by throwing an exception.
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("/invalid"))
                {
                    throw new Exception("ERROR!");
                }
                await next();
            });

            // UseFileServer is the method initialises static file hosting
            // from the folder 'wwwroot'.
            app.UseFileServer();
        }
    }
}
