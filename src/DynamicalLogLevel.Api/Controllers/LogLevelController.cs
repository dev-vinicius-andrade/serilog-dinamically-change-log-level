using System.Text.Json.Serialization;
using DynamicalLogLevel.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;

namespace DynamicalLogLevel.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogLevelController : ControllerBase
    {
        private readonly ILogger<LogLevelController> _logger;
        private readonly IDinamicallyLogSwitcherService _dinamicallyLogSwitcherService;

        public LogLevelController(ILogger<LogLevelController> logger, IDinamicallyLogSwitcherService dinamicallyLogSwitcherService)
        {
            _logger = logger;
            _dinamicallyLogSwitcherService = dinamicallyLogSwitcherService;
        }

        [HttpPost("{switchName}/{logLevel}")]
        public IActionResult Post(string switchName,  LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _dinamicallyLogSwitcherService.Switch(switchName, new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Verbose));
                    return Ok($"Switched {switchName} to {logLevel}");
                case LogLevel.Debug:
                    _dinamicallyLogSwitcherService.Switch(switchName, new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Debug));
                    return Ok($"Switched {switchName} to {logLevel}");
                case LogLevel.Information:
                    _dinamicallyLogSwitcherService.Switch(switchName, new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Information));
                    return Ok($"Switched {switchName} to {logLevel}");
                case LogLevel.Warning:
                    _dinamicallyLogSwitcherService.Switch(switchName, new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Warning));
                    return Ok($"Switched {switchName} to {logLevel}");
                case LogLevel.Error:
                    _dinamicallyLogSwitcherService.Switch(switchName, new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Error));
                    return Ok($"Switched {switchName} to {logLevel}");
                case LogLevel.Critical:
                    _dinamicallyLogSwitcherService.Switch(switchName, new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Fatal));
                    return Ok($"Switched {switchName} to {logLevel}");
                case LogLevel.None:
                    _dinamicallyLogSwitcherService.Switch(switchName, new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Fatal));
                    return Ok($"Switched {switchName} to {logLevel}");
                default:
                    return BadRequest($"Log level {logLevel} is not valid");
            }
        }
        [HttpGet("{switchName}")]
        public IActionResult Get(string switchName)
        {
            return Ok(_dinamicallyLogSwitcherService.GetSwitch(switchName));
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dinamicallyLogSwitcherService.GetAllSwitchNames());
        }
    }
}