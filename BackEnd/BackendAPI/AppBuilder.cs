namespace BackendAPI
{
    public static class AppBuilder
    {
        public static IHostBuilder ConfigureAppSettings(this IHostBuilder hostBuilder)
        {
            var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT");

            hostBuilder.ConfigureAppConfiguration((ctx, builder) =>
            {
                builder.AddJsonFile($"appsettings.json", false, true);
                builder.AddJsonFile($"appsettings.{enviroment}.json", true, true);
                builder.AddEnvironmentVariables();
            });

            return hostBuilder;
        }
    }
}
