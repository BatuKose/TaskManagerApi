using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using System.Security.Cryptography.X509Certificates;

namespace TaskManagerApi.Extensions
{
    public static class ServiceExtensions
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
        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager,ServiceManager>();
        }
    }
}
