using System.Collections;
using DynamicalLogLevel.Api.Interfaces;
using Serilog.Core;

namespace DynamicalLogLevel.Api.Services;

internal class DinamicallyLogSwitcherService:IDinamicallyLogSwitcherService
{
    private readonly IDictionary<string, LoggingLevelSwitch> _switches;

    private DinamicallyLogSwitcherService(IDictionary<string, LoggingLevelSwitch>? switches=null)
    {
        _switches = switches ?? new Dictionary<string, LoggingLevelSwitch>();
    }

    public static IDinamicallyLogSwitcherService Create(IDictionary<string, LoggingLevelSwitch>? switches = null) =>
        new DinamicallyLogSwitcherService(switches);


    public IEnumerable<string> GetAllSwitchNames()
    {
        return !_switches.Any() ? Enumerable.Empty<string>() : _switches.Keys;
    }

    public void Switch(string switchName, LoggingLevelSwitch levelSwitch)
    {
        if (_switches.ContainsKey(switchName))
            _switches[switchName] = levelSwitch;
    }

    public void AddSwitch(string switchName, LoggingLevelSwitch levelSwitch)
    {
        if (!_switches.ContainsKey(switchName))
            _switches.Add(switchName, levelSwitch);
    }

    public LoggingLevelSwitch? GetSwitch(string switchName)
    {
        return !_switches.ContainsKey(switchName) ? null : _switches[switchName];
    }
}