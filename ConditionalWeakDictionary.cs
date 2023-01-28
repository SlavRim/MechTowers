namespace MechTowers;

public class ConditionalWeakDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    where TKey : class
    where TValue : class
{
    private readonly ConditionalWeakTable<TKey, TValue> table = new();

    public TValue this[TKey key]
    {
        get => Get(key);
        set => Set(key, value);
    }

    public TValue Get(TKey key) => TryGetValue(key, out var value) ? value : null;
    public void Set(TKey key, TValue value)
    {
        if (key is null) return;
        Remove(key);
        Add(key, value);
    }

    public ICollection<TKey> Keys => table.Keys;

    public ICollection<TValue> Values => table.Values;

    public int Count => table.Keys.Count;

    public bool IsReadOnly => false;

    public bool ContainsKey(TKey key) => Keys.Contains(key);

    public bool TryGetValue(TKey key, out TValue value) => table.TryGetValue(key, out value);

    public IEnumerable<KeyValuePair<TKey, TValue>> AsEnumerable()
    {
        foreach (var key in Keys)
            yield return new(key, this[key]);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        foreach (var item in AsEnumerable())
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public void Add(TKey key, TValue value)
    {
        if (key is null) return;
        table.Add(key, value);
    }
    public bool Remove(TKey key)
    {
        if (key is null) return false;
        return table.Remove(key);
    }
    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.key, item.value);
    public void Clear() => table.Clear();
    public bool Contains(KeyValuePair<TKey, TValue> item) => ContainsKey(item.key);
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => AsEnumerable().ToArray().CopyTo(array, arrayIndex);
    public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
}