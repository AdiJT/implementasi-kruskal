using Kruskal.Core;

var minQueue = new MinHeapQueue<int, int>(i => i, [100, 90, 80, 70, 0]);

Console.WriteLine(minQueue.Dequeue());
Console.WriteLine(minQueue.Dequeue());
Console.WriteLine(minQueue.Dequeue());
Console.WriteLine(minQueue.Dequeue());
Console.WriteLine(minQueue.Dequeue());