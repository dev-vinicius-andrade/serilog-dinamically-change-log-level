using Serilog.Core;

namespace DynamicalLogLevel.Api.Interfaces;

public interface IDinamicallyLogSwitcherService
{
    IEnumerable<string> GetAllSwitchNames();
    LoggingLevelSwitch? this[string switchName] => GetSwitch(switchName);
    void Switch(string switchName, LoggingLevelSwitch levelSwitch);
    void AddSwitch(string switchName, LoggingLevelSwitch levelSwitch);
    LoggingLevelSwitch? GetSwitch(string switchName);
}