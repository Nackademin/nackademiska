using AddressBook.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nackademiska.Services;

namespace Nackademiska
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials() );
            });
            // services.Configure<MvcOptions>(options =>
            // {
            //     options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            // });
            services.AddMvc();

            services.AddSingleton<IAuctionRepository, AuctionDatabaseRepository>();
            services.AddSingleton<ICustomerRepository, CustomerDatabaseRepository>();
            services.AddSingleton<ISupplierRepository, SupplierDatabaseRepository>();
            services.AddSingleton<IAdminRepository, AdminDatabaseRepository>();

            services.AddTransient<ApplicationDbContextSeeder>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContextSeeder seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseCors(builder =>  builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseCors("CorsPolicy");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();

            seeder.SeedData();
        }
    }
}
