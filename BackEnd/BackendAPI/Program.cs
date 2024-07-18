using BackendAPI;
using BackendAPI.Middlewares;
using DOMAIN.Enums;
using DOMAIN.Shared;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//extension method
builder.Host.ConfigureAppSettings();

//extension method
builder.Services.AddServices(builder.Configuration);

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
else
{
    app.UseSerilogRequestLogging();
}

app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");

app.UseAuthentication();

app.UseAuthorization();

//middlewares
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();


