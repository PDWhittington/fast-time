using System.Diagnostics.CodeAnalysis;

namespace FastTime;

[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public unsafe interface IClock
{
    Timestamp GetTimestamp();

    SystemTime GetSystemTimePreciseAsFileTime();

    BasedTimestamp GetBasedTimestamp();

    delegate*<long*, void> GetPerformanceFreqPtr();

    delegate*<Timestamp*, void> GetTimestampPtr();

    delegate*<SystemTime*, void> GetSystemTimePreciseAsFileTimePtr();

    delegate*<BasedTimestamp*, void> GetBasedTimestampPtr();
}
