public class Status : ValueObject
{
    public int Value { get; }

    public Status(int status)
    {
        Value = status;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}