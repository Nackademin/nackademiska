using AddressBook.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Nackademiska.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Nackademiska.Models;

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
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                
                // Cookie settings
                options.Cookies.ApplicationCookie.Events = 
                    new CookieAuthenticationEvents() 
                    {
                        OnRedirectToLogin = (context) => {
                            context.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = (context) => {
                            context.Response.StatusCode = 403;
                            return Task.CompletedTask;
                        }
                    };
            });
            services.AddCors();
            // services.AddCors(options =>
            // {
            //     options.AddPolicy("CorsPolicy",
            //         builder => builder.AllowAnyOrigin()
            //         .AllowAnyMethod()
            //         .AllowAnyHeader()
            //         .AllowCredentials() );
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
            
            app.UseApplicationInsightsRequestTelemetry();
            //app.UseCors(builder =>  builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseIdentity();
            app.UseJwtBearerAuthentication(new JwtBearerOptions() 
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://nackademiska.azurewebsites.net",
                    ValidAudience = "http://nackademiska.azurewebsites.net",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nackademiskaAuktionsfrämjandet")),
                    ValidateLifetime = true
                }
            });
            
            // app.UseCors("CorsPolicy");
            app.UseCors(cfg => 
            {
                cfg.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
            app.UseMvc();

            seeder.SeedData();
        }
    }
}
