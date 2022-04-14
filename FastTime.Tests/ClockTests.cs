using Xunit;
using Xunit.Abstractions;

namespace FastTime.Tests;

public class ClockTests : IClassFixture<ClockFixture>
{
    private readonly ClockFixture _fixture;
    private readonly ITestOutputHelper _testOutputHelper;

    public ClockTests(ClockFixture fixture, ITestOutputHelper testOutputHelper)
    {
        _fixture = fixture;
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public unsafe void GetPerformanceFrequency()
    {
        var fnPtr = _fixture.Clock.GetPerformanceFreqPtr();

        long performanceFreq;

        fnPtr(&performanceFreq);

        _testOutputHelper.WriteLine($"Performance Frequency: {performanceFreq}");
    }

    [Fact]
    public unsafe void GetImpliedStartTime()
    {
        var fnPtr = _fixture.Clock.GetBasedTimestampPtr();

        BasedTimestamp basedTimestamp;

        fnPtr(&basedTimestamp);

        _testOutputHelper.WriteLine($"Before: {basedTimestamp.TimestampBefore}");
        _testOutputHelper.WriteLine($"SystemTime: {basedTimestamp.SystemTime}");
        _testOutputHelper.WriteLine($"After: {basedTimestamp.TimestampAfter}");
        _testOutputHelper.WriteLine($"After - Before: {basedTimestamp.TimestampAfter.AsLong() - basedTimestamp.TimestampBefore.AsLong()}");
        _testOutputHelper.WriteLine($"Implied start time: {basedTimestamp.ImpliedStartTime()}");
    }


    [Fact]
    public unsafe void GetBasedTimestamp()
    {
        var fnPtr = _fixture.Clock.GetBasedTimestampPtr();

        BasedTimestamp basedTimestamp;

        fnPtr(&basedTimestamp);

        _testOutputHelper.WriteLine($"Before: {basedTimestamp.TimestampBefore}");
        _testOutputHelper.WriteLine($"SystemTime: {basedTimestamp.SystemTime}");
        _testOutputHelper.WriteLine($"After: {basedTimestamp.TimestampAfter}");
        _testOutputHelper.WriteLine($"After - Before: {basedTimestamp.TimestampAfter.AsLong() - basedTimestamp.TimestampBefore.AsLong()}");
    }

    [Fact]
    public void GetSystemTimePreciseAsFileTime()
    {
        var result = _fixture.Clock.GetSystemTimePreciseAsFileTime();

        _testOutputHelper.WriteLine($"{result.AsDateTime()}");
    }

    [Fact]
    public unsafe void GetTimestamp()
    {
        var performanceCount = _fixture.Clock.GetTimestamp();

        _testOutputHelper.WriteLine($"{performanceCount.AsLong()}");

        performanceCount = _fixture.Clock.GetTimestamp();
        var performanceCount2 = _fixture.Clock.GetTimestamp();

        _testOutputHelper.WriteLine($"{performanceCount2.AsLong() - performanceCount.AsLong()}");

        var fnPtr = _fixture.Clock.GetTimestampPtr();

        fnPtr(&performanceCount);
        fnPtr(&performanceCount2);

        _testOutputHelper.WriteLine($"{performanceCount2.AsLong() - performanceCount.AsLong()}");
    }
}
