using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.Contracts;
using Repositories.EFCore;
using Serilog;
using Services;
using Services.Contracts;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IUserIzınRepository, userIzınRepository>();
         
        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager,ServiceManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IUserService,UserManager>();
            services.AddScoped<IJobHeaderService,JobHeaderManager>();
            services.AddScoped<IJobDetailService,JobDetailManager>();
            services.AddScoped<IAuthenticationService,AuthenticationManager>();
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
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwt = configuration.GetSection("jwt");
            var Key = Encoding.UTF8.GetBytes(jwt["Key"]);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
                (
                    opt =>
                    {
                        opt.TokenValidationParameters= new TokenValidationParameters
                        {
                            ValidateIssuer=true,
                            ValidateAudience=true,
                            ValidateIssuerSigningKey=true,
                            ValidateLifetime=true,
                            ValidIssuer=jwt["Issuer"],
                            ValidAudience=jwt["Audience"],
                            IssuerSigningKey= new SymmetricSecurityKey(Key)
                        };
                    }
                );
            return services;
        }
        public static IServiceCollection AddSwaggerWithJwtAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TaskManager API",
                    Version = "v1"
                });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Bearer token giriniz. Örnek: Bearer {token}",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };
                c.AddSecurityDefinition("Bearer", securityScheme);
                var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            };

                c.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }
        public static IServiceCollection  AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireClaim("RoleId", "1"));

                options.AddPolicy("Manager", policy =>
                    policy.RequireClaim("RoleId", "2","1"));
                options.AddPolicy("Worker", policy =>
                    policy.RequireClaim("RoleId", "3", "1"));

                options.AddPolicy("Admin-Manager", policy =>
                    policy.RequireClaim("RoleId", "1", "2"));

                options.AddPolicy("Worker-Manager", policy =>
                    policy.RequireClaim("RoleId", "3", "2","1"));
            });
            return services;
        }

    }
}
