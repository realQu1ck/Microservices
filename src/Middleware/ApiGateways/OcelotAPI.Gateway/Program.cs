using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, options) =>
{
    options.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
});
builder.Host.ConfigureLogging((options, builder) =>
{
    builder.AddConfiguration(options.Configuration.GetSection("Logging"));
    builder.AddConsole();
    builder.AddDebug();
});

builder.Services.AddOcelot();
var app = builder.Build();
app.UseOcelot();
app.MapGet("/", () => "Hello World!");

app.Run();