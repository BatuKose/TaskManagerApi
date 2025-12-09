using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Serilog;
using Services;
using Services.Contracts;
using System.Security.Cryptography.X509Certificates;

namespace TaskManagerApi.Extensions
{
    public static partial class ServiceExtensions
    {   
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration)
        {   
        {
            services.AddDbContext<RepositoryContext>(
                 options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection")
               , m=>m.MigrationsAssembly("TaskManagerApi")
                 ));
        }
          
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleReposity>();
            services.AddScoped<IJobHeaderRepository, JobHeaderRepository>();
            services.AddScoped<IjobDetailRepository,jobDetailRepository>();
        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager,ServiceManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IUserService,UserManager>();
            services.AddScoped<IJobHeaderService,JobHeaderManager>();
            services.AddScoped<IJobDetailService,JobDetailManager>();
        }

        public static void AddSerilogLogging(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Debug()
                .CreateLogger();

            builder.Host.UseSerilog();
        }
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
        }
        public static IServiceCollection AddCustomRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimit"));
            services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
            services.AddInMemoryRateLimiting();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            return services;
        }
        public static IApplicationBuilder UseCustomRateLimiting(this IApplicationBuilder app)
        {
            app.UseIpRateLimiting();
            return app;
        }
    }
}
