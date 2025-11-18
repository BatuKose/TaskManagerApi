using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;

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
    }
}
