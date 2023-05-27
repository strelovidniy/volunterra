using VolunteerManager.Server.DependencyInjection;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .ReadFrom
        .Configuration(builder.Configuration)
        .CreateLogger();

    builder.Services.RegisterApplication(builder.Configuration);

    var app = builder.Build();

    app.UseApplication();

    app.Run();
}
catch (Exception exception)
{
    Log.Logger.Error(exception, "Stopped program because of exception");
}
finally
{
    await Log.CloseAndFlushAsync();
}