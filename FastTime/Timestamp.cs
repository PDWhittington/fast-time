namespace FastTime;

public readonly struct Timestamp
{
#pragma warning disable CS0649 // _value is assigned with unsafe pointer logic
    private readonly long _value;
#pragma warning restore CS0649

    public long AsLong() => _value;

    public override string ToString() => _value.ToString();

    public override int GetHashCode() => _value.GetHashCode();

    public override bool Equals(object? obj)
        => obj is Timestamp casted && _value == casted._value;
}
