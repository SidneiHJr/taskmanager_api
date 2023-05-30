using TaskManager.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);

var app = builder.Build();

Configure(app);

app.Run();


void ConfigureServices (IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();

    services.AddSwaggerConfig();
}

void Configure(WebApplication app)
{
    app.UseSwaggerConfig();

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

}