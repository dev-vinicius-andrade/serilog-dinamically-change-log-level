using DynamicalLogLevel.Api;
using DynamicalLogLevel.Api.Interfaces;
using DynamicalLogLevel.Api.Services;
using Microsoft.Extensions.DependencyModel;
using Serilog;
using Serilog.Core;
using Serilog.Settings.Configuration;
using System.Text.Json.Serialization;

IDinamicallyLogSwitcherService ConfigureSerilog(IHostBuilder hostBuilder, ILoggingBuilder loggerBuilder)
{
    Serilog.Debugging.SelfLog.Enable(Console.Error);
    var dinamicallyLogSwitcherService = DinamicallyLogSwitcherService.Create();
    loggerBuilder.ClearProviders();
    hostBuilder.UseSerilog((context, configuration) =>
    {
        
        var options = new ConfigurationReaderOptions
        {
            OnLevelSwitchCreated = (switchName, levelSwitch) => dinamicallyLogSwitcherService.AddSwitch(switchName, levelSwitch)
        };
        configuration.ReadFrom.Configuration(context.Configuration, options);
    });
    return dinamicallyLogSwitcherService;
}

var builder = WebApplication.CreateBuilder(args);
var dinamicallyLogSwitcherService = ConfigureSerilog(builder.Host, builder.Logging);
builder.Services.AddSingleton(dinamicallyLogSwitcherService);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
