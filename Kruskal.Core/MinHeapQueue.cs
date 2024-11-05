using System.Runtime.InteropServices;

namespace Kruskal.Core;

public class MinHeapQueue<T, TKey>
    where TKey : IComparable<TKey>
{
    private readonly List<T> _queue = [];
    private readonly Func<T, TKey> _keySelector;

    public int Count => _queue.Count;

    public MinHeapQueue(Func<T, TKey> keySelector)
    {
        _keySelector = keySelector;
    }

    public MinHeapQueue(Func<T, TKey> keySelector, IEnumerable<T> items)
    {
        _keySelector = keySelector;
        _queue = items.ToList();

        BuildMinHeap();
    }

    public bool Contains(T item) => _queue.Contains(item);

    public T? Find(Predicate<T> match) => _queue.Find(match);

    public void Enqueue(T item)
    {
        _queue.Add(item);
        ShiftUp();
    }

    public T Dequeue()
    {
        var item = _queue[0];
        (_queue[0], _queue[Count - 1]) = (_queue[Count - 1], _queue[0]);
        _queue.RemoveAt(Count - 1);
        MinHeapify(0);
        return item;
    }

    public T Peek() => _queue[0];

    private int Compare(T x, T y) => _keySelector(x).CompareTo(_keySelector(y));

    private void BuildMinHeap()
    {
        for (int i = Count / 2 - 1; i >= 0; i--)
            MinHeapify(i);
    }

    private void ShiftUp()
    {
        var item = _queue[_queue.Count - 1];
        var bestPos = Count - 1;
        var parent = (int)((bestPos / 2d) - (1d / 2d));
        var childPos = bestPos;

        while (bestPos > 0 && Compare(item, _queue[parent]) < 0)
        {
            childPos = bestPos;
            bestPos = parent;
            parent = (int)((bestPos / 2d) - (1d / 2d));
        }

        if (bestPos != Count - 1)
        {
            (_queue[bestPos], _queue[Count - 1]) = (_queue[Count - 1], _queue[bestPos]);
            MinHeapify(childPos);
        }
    }

    private void MinHeapify(int pos)
    {
        var leftChild = pos * 2 + 1;
        var rightChild = pos * 2 + 2;

        var smallest = pos;
        if (leftChild < Count && Compare(_queue[leftChild], _queue[smallest]) < 0)
            smallest = leftChild;

        if (rightChild < Count && Compare(_queue[rightChild], _queue[smallest]) < 0)
            smallest = rightChild;

        if (smallest != pos)
        {
            (_queue[pos], _queue[smallest]) = (_queue[smallest], _queue[pos]);
            MinHeapify(smallest);
        }
    }
}