using Microsoft.EntityFrameworkCore;
using LearningAspDotNetCoreMVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Identity;

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
        // This method gets called by the runtime. Use this method to add services
        // to the container. For more information on how to configure your
        // application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // To use the FormattingServices injectable service, it must be
            // configured here first. 
            services.AddTransient<FormattingService>();
            
            // To tell ASP.NET how to instantiate the FeatureToggles class, use
            // the AddScoped, AddSingleton or AddTransient methods when configuring
            // the service:
            // AddScoped - will only create one instance for each web request
            // AddSingleton - to share across requests. one single instance
            // AddTransient - short lifespan, a new instance each time requested

            // We use an anonymous function to obtain the configuration data
            services.AddTransient<FeatureToggles>(x => new FeatureToggles
            {
                DeveloperExceptions =
                    configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
            });

            // Configure the database context for BlogDbContext
            services.AddDbContext<BlogDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("BlogDataContext");
                // UseSqlServer requires installation of the NuGet package
                // Microsoft.EntityFrameworkCore.SqlServer .
                options.UseSqlServer(connectionString);
            });
            
            // Configure the database context for IdentityDbContext
            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("BlogDataContext");
                options.UseSqlServer(connectionString);
            });

            // Configure the Identity framework
            // Before using the Identity Framework, the corresponding tables need
            // to be generated via database migration:
            // 1. Ensure NuGet package Microsoft.EntityFrameworkCore.Tools is added.
            // 2. Change the connection string to point to a new DB.
            // 3. In the package manager console:
            // 3a. - 'enable-migrations'
            // 3b. - 'add-migration InitialCreate -context IdentityDataContext'
            // 3c. - Check that tables have been created successfully in the db.

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();

            // Register the MCV design pattern.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure
        // the HTTP request pipeline. The features parameter brings in settings
        // defined in the FeatureToggles class for use in the project. 
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            FeatureToggles features)
        {
            // Use the exception handler to serve a custom error page
            // (providing that the environment variable ASPNETCORE_ENVIRONMENT
            // is set to "Development").
            app.UseExceptionHandler("/error.html");

            // Alternatively we can use our own environment variable in
            // conjunction with the configuration in the Startup object.
            // The configuration API can navigate complex objects in the
            // appsettings.json file (children are accesses using a : ).

            // The configuration API will first look for the environment
            // variable ASPNETCORE_ENVIRONMENT being set to "Development".
            // If this is the case, the appsettings.Development.json file
            // is read. If not, it uses appsettings.json for settings.

            // Now that there is a class containing the settings, we can
            // access settings through a FeatureToggles object instantiated
            // as 'features'.
            if (features.DeveloperExceptions) 
            {
                app.UseDeveloperExceptionPage();
            }

            // This code tests the error handling by throwing an exception.
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("/invalid"))
                {
                    throw new Exception("ERROR!");
                }
                await next();
            });

            // Register the Identity framework to allow authorization functionality.
            // This must be placed before mvc / routing.
            // Note: This requires installation of the NuGet package 
            // Microsoft.AspNetCore.Identity.EntityFrameworkCore .
            // In .NET Core 5.0, UseAuthentication() method replaces UseIdentity() .
            // Reference: https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x?view=aspnetcore-5.0
            app.UseAuthentication();

            // Assign the URL to be mapped when accessing the MVC pages
            // - differs from course, using code from class instead.
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // Specifying 'Home' will look for the HomeController.cs
                // controller and the Index() action method.
                // Note: Specific constraints can be specified for the URL
                // parameters, e.g: {id:int?} - optional but must be int
                endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            // UseFileServer is the method initialises static file hosting
            // from the folder 'wwwroot'.
            app.UseFileServer();
        }
    }
}
