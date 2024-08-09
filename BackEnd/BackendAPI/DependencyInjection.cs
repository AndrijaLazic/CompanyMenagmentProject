using BLL.Services;
using DAL;
using DOMAIN.Abstractions;
using DOMAIN.Enums;
using DOMAIN.Models.Database;
using DOMAIN.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["MyAppSettings:JWTSettings:ISSUER"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["MyAppSettings:JWTSettings:SECRET_KEY"]!)),
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserIsAdmin", p =>
                {
                    p.RequireClaim("workerType", ((int)EWorkerTypes.Admin).ToString());
                });
            });

            // Add services to the container.

            services.AddScoped<IUserDataDB,UserDataDB>();
            services.AddScoped<CommunicationDB, CommunicationDB>();
            services.AddScoped<WorkCalendarDB, WorkCalendarDB>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<AdminService, AdminService>();
            services.AddScoped<WorkerService, WorkerService>();
            services.AddScoped<CommunicationService, CommunicationService>();
            services.AddSingleton<GlobalDataService,GlobalDataService>();
            services.AddSingleton<SharedDB, SharedDB>();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });


            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins(appConfig.AllowedHosts)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
   

            return services;
        }
    }
}
