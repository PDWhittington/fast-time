using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace FastTime;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public unsafe class Clock : IClock
{
    #region DLL Imports

    [DllImport("Kernel32.dll")]
    private static extern bool QueryPerformanceCounter(long* lpPerformanceCount);

    [DllImport("Kernel32.dll")]
    private static extern bool QueryPerformanceFrequency(long* lpFrequency);

    [DllImport("Kernel32.dll")]
    private static extern void GetSystemTimePreciseAsFileTime(long* lpSystemTimeAsFileTime);

    #endregion

    private static long _performanceFrequency;

    public Clock()
    {
        long performanceFrequency;

        QueryPerformanceFrequency(&performanceFrequency);
        _performanceFrequency = performanceFrequency;
    }

    #region Instance methods

    public Timestamp GetTimestamp()
    {
        long timestamp;

        QueryPerformanceCounter(&timestamp);

        return *(Timestamp*) (&timestamp);
    }

    public long QueryPerformanceFrequency()
    {
        long lpFrequency;
        QueryPerformanceFrequency(&lpFrequency);
        return lpFrequency;
    }

    public SystemTime GetSystemTimePreciseAsFileTime()
    {
        SystemTime systemTime;
        GetSystemTimePreciseAsFileTimeStatic(&systemTime);
        return systemTime;
    }

    public BasedTimestamp GetBasedTimestamp()
    {
        BasedTimestamp basedTimestamp;

        GetBasedTimestampStatic(&basedTimestamp);
        return basedTimestamp;
    }

    #endregion

    #region Fast method pointers

    public delegate*<long*, void> GetPerformanceFreqPtr() => &GetPerformanceFreqStatic;

    public delegate*<Timestamp*, void> GetTimestampPtr() => &GetTimestampStatic;

    public delegate*<SystemTime*, void> GetSystemTimePreciseAsFileTimePtr() => &GetSystemTimePreciseAsFileTimeStatic;

    public delegate*<BasedTimestamp*, void> GetBasedTimestampPtr() => &GetBasedTimestampStatic;

    #endregion

    #region Static methods

    private static void GetPerformanceFreqStatic(long* timestamp) => QueryPerformanceFrequency(timestamp);

    private static void GetTimestampStatic(Timestamp* timestamp) => QueryPerformanceCounter((long*) timestamp);

    private static void GetSystemTimePreciseAsFileTimeStatic(SystemTime* systemTime) => GetSystemTimePreciseAsFileTime((long*) systemTime);

    private static void GetBasedTimestampStatic(BasedTimestamp* basedTimestamp)
    {
        Timestamp before, after;
        SystemTime systemTime;

        GetTimestampStatic(&before);
        GetSystemTimePreciseAsFileTimeStatic(&systemTime);
        GetTimestampStatic(&after);

        var result = BasedTimestamp.Of(before, systemTime, after, _performanceFrequency);
        *basedTimestamp = result;
    }

    #endregion
}
