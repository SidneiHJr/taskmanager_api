using Microsoft.Extensions.Configuration;
using TaskManager.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json",true,true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app);

app.Run();


void ConfigureServices (IServiceCollection services, IConfiguration configuration)
{

    services.AddEndpointsApiExplorer();

    services.AddSwaggerConfig();
    services.AddApiConfig(configuration);
}

void Configure(WebApplication app)
{
    app.UseSwaggerConfig();
    app.UseApiConfig();

}