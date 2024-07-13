using BackendAPI;
using DOMAIN.Shared;

var builder = WebApplication.CreateBuilder(args);

//extension method
builder.Host.ConfigureAppSettings();

var appConfig = builder.Configuration.Get<AppConfigClass>();
Console.WriteLine(appConfig.Database.ConnectionString);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
