using BLL.Services;
using DAL;
using DOMAIN.Abstractions;
using DOMAIN.Models.Database;
using DOMAIN.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackendAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppConfigClass>(configuration.GetSection("MyAppSettings"));
            var appConfig = configuration.GetSection("MyAppSettings").Get<AppConfigClass>();

            services.AddDbContext<CompanyMenagmentProjectContext>(
                options => options.UseSqlServer(appConfig.Database.ConnectionString));

            

            // Add services to the container.

            services.AddScoped<IUserDataDB,UserDataDB>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins(appConfig.AllowedHosts)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
   

            return services;
        }
    }
}
