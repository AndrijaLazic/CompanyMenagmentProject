using BLL.Services;
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
            var appConfig = configuration.Get<AppConfigClass>();


            // Add services to the container.

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



            services.AddDbContext<CompanyMenagmentProjectContext>(
                options => options.UseSqlServer(appConfig.Database.ConnectionString));

            return services;
        }
    }
}
