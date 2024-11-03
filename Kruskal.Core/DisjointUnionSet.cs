namespace Kruskal.Core;

public class DisjointUnionSet<T>
    where T : IEquatable<T>
{
    private readonly List<T> _items;
    private readonly List<int> _parent;
    private readonly List<int> _rank;

    public DisjointUnionSet(IEnumerable<T> items)
    {
        _items = items.ToList();
        _parent = Enumerable.Range(0, _items.Count).ToList();
        _rank = Enumerable.Repeat(0, _items.Count).ToList();
    }

    public int Find(int index)
    {
        if(index < 0 || index >= _items.Count)
            throw new ArgumentOutOfRangeException("index out of range");

        if (_parent[index] == index)
            return index;
        else
        {
            var parent = _parent[index];
            _parent[index] = parent;
            return parent;
        }
    }

    public int FindByValue(T value)
    {
        var index = _items.IndexOf(value);
        if (index == -1)
            throw new ArgumentException("value not in set");

        return Find(index);
    }

    public bool Union(int index1, int index2)
    {
        var index1Rep = Find(index1);
        var index2Rep = Find(index2);

        if (index1Rep == index2Rep)
            return false;

        if (_rank[index1Rep] < _rank[index2Rep])
            _parent[index1Rep] = index2Rep;
        else if (_rank[index1Rep] > _rank[index2Rep])
            _parent[index2Rep] = index1Rep;
        else
        {
            _parent[index1Rep] = index2Rep;
            _rank[index2Rep]++;
        }

        return true;
    }

    public bool UnionByValue(T value1, T value2)
    {
        var index1 = _items.IndexOf(value1);
        var index2 = _items.IndexOf(value2);

        if (index1 == -1)
            throw new ArgumentException("value1 not in set");

        if (index2 == -1)
            throw new ArgumentException("value2 not in set");

        return Union(index1, index2);
    }
}
