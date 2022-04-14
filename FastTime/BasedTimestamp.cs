using System.Diagnostics.CodeAnalysis;

namespace FastTime;

[SuppressMessage("ReSharper", "NotAccessedField.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public readonly struct BasedTimestamp
{
    public readonly Timestamp TimestampBefore;
    public readonly SystemTime SystemTime;
    public readonly Timestamp TimestampAfter;
    public readonly long PerformanceFrequency;

    private BasedTimestamp(Timestamp before, SystemTime systemTime, Timestamp after, long performanceFrequency)
    {
        TimestampBefore = before;
        SystemTime = systemTime;
        TimestampAfter = after;
        PerformanceFrequency = performanceFrequency;
    }

    public static BasedTimestamp Of(Timestamp before, SystemTime systemTime, Timestamp after, long performanceFrequency)
        => new(before, systemTime, after, performanceFrequency);

    public SystemTime ImpliedStartTime()
    {
        return (SystemTime.AsLong() - (TimestampBefore.AsLong() >> 1) - (TimestampAfter.AsLong() >> 1)).ToSystemTime();
    }

    public SystemTime ImpliedStartTimeFromAfter()
    {
        return (SystemTime.AsLong() - TimestampAfter.AsLong()).ToSystemTime();
    }
}
