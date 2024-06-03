namespace Egress.Domain;

public class CountGroupBy<TKey, TValue>
{
    public TKey? Key { get; set; }

    public TValue? Value { get; set; }
}
