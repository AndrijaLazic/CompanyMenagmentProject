using Serilog;

namespace BackendAPI
{
    public static class AppBuilder
    {
        public static IHostBuilder ConfigureAppSettings(this IHostBuilder hostBuilder)
        {
            var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            hostBuilder.ConfigureAppConfiguration((ctx, builder) =>
            {
                builder.AddJsonFile($"appsettings.json", false, true);
                builder.AddJsonFile($"appsettings.{enviroment}.json", true, true);
                builder.AddEnvironmentVariables();
            });

            //Serilog config
            if (!enviroment.Equals("Development"))
            {
                hostBuilder.UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                });
            }
            

            return hostBuilder;
        }
    }
}
