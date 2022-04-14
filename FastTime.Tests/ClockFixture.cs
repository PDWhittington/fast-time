using System.Diagnostics.CodeAnalysis;

namespace FastTime.Tests;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class ClockFixture
{
    public Clock Clock { get; }

    public ClockFixture()
    {
        Clock = new Clock();
    }
}
