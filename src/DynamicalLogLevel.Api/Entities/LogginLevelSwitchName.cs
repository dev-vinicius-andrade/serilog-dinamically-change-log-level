namespace DynamicalLogLevel.Api.Entities;

public class LogginLevelSwitchName
{
    private readonly string _value;
    private LogginLevelSwitchName(string value)
    {
        _value = value;
    }
    public static implicit operator string(LogginLevelSwitchName name) => name._value;
    public static implicit operator LogginLevelSwitchName(string name) => new(name);
}