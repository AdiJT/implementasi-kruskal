using Kruskal.Core;

var minQueue = new MinHeapQueue<int, int>(i => i);

minQueue.Enqueue(100);
minQueue.Enqueue(90);
minQueue.Enqueue(80);
minQueue.Enqueue(70);
minQueue.Enqueue(60);

Console.WriteLine(minQueue.Dequeue());
Console.WriteLine(minQueue.Dequeue());
Console.WriteLine(minQueue.Dequeue());
Console.WriteLine(minQueue.Dequeue());
Console.WriteLine(minQueue.Dequeue());