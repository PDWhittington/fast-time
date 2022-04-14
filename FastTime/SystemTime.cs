using System;
using System.Diagnostics.CodeAnalysis;

namespace FastTime;

public readonly struct SystemTime
{
    private readonly long _value;

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public SystemTime(long fileTime)
    {
        _value = fileTime;
    }

    public DateTime AsDateTime() => DateTime.FromFileTime(_value);

    public long AsLong() => _value;

    public override string ToString() => DateTime.FromFileTime(_value).ToString("yyyy-MM-dd HH:mm:ss.fffffff");
}

public static class SystemTimeExtensions
{
    public static unsafe SystemTime ToSystemTime(this long fileTime)
        => *(SystemTime*) (&fileTime);
}
