using System.Drawing;
using System.Numerics;

namespace Kruskal.Core;

public class Graph<T> where T : IEquatable<T>
{
    private readonly List<Vertex<T>> _vertices;

    public IReadOnlyList<Vertex<T>> Vertices => _vertices;
    public IReadOnlyList<Edge<T>> Edges => _vertices
         .SelectMany((v) => v.AdjencyList.Select(adj => new Edge<T>(v, adj.adj, adj.weight)))
         .ToList();

    public IReadOnlyList<Edge<T>> EdgesDistinct
    {
        get
        {
            var edges = Edges;

            var result = new List<Edge<T>>();

            foreach (var edge in edges)
                if (!result.Contains(edge))
                    result.Add(edge);

            return result;
        }
    }

    public double TotalWeight => EdgesDistinct.Aggregate(0d, (acc, e) => acc + e.Weight);

    public Graph(List<T> vertices, List<(T v1, T v2, double weight)> edges)
        : this(vertices.Select(v => new Vertex<T>(v)).ToList(), edges.Select(e => new Edge<T>(e.v1, e.v2, e.weight)).ToList())
    {
    }

    public Graph(List<Vertex<T>> vertices, List<Edge<T>> edges)
    {
        _vertices = vertices
            .Distinct()
            .Select(v => new Vertex<T>(v.Value, v.Position, Vector2.Zero, []))
            .ToList();

        foreach (var edge in edges)
        {
            var indexV1 = _vertices.IndexOf(edge.V1);
            var indexV2 = _vertices.IndexOf(edge.V2);

            if (indexV1 == -1 || indexV2 == -1)
                throw new ArgumentException($"no vertex {edge.V1} or {edge.V2} in vertices");

            if (edge.Weight < 0)
                throw new ArgumentException("negatif edge weight");

            if (_vertices[indexV1].AdjencyList.FindIndex(adj => adj.adj == edge.V2) == -1)
                _vertices[indexV1].AdjencyList.Add((_vertices[indexV2], edge.Weight));

            if (_vertices[indexV2].AdjencyList.FindIndex(adj => adj.adj == edge.V1) == -1)
                _vertices[indexV2].AdjencyList.Add((_vertices[indexV1], edge.Weight));
        }
    }

    public Graph(Graph<T> graph)
        : this([..graph.Vertices], [..graph.EdgesDistinct])
    {
    }

    public double EdgeCost(Vertex<T> a, Vertex<T> b)
    {
        var indexA = _vertices.IndexOf(a);
        var adjIndexB = _vertices[indexA].AdjencyList.FindIndex(adj => adj.adj == b);

        if (adjIndexB == -1)
            return double.PositiveInfinity;
        else
            return _vertices[indexA].AdjencyList[adjIndexB].weight;
    }

    public int AddVertex(Vertex<T> v)
    {
        if (_vertices.Contains(v))
            return _vertices.FindIndex(x => x == v);

        _vertices.Add(v);
        return _vertices.Count - 1;
    }

    public void AddEdge(Edge<T> edge)
    {
        if (EdgesDistinct.Contains(edge))
            throw new ArgumentException($"Sudah ada edge antara {edge.V1.Value} dan {edge.V2.Value}");

        if (edge.V1 == edge.V2)
            throw new ArgumentException($"Sisi adalah loop pada {edge.V1.Value}");

        if (edge.Weight < 0)
            throw new ArgumentException("weight negatif");

        var indexV1 = _vertices.IndexOf(edge.V1);
        var indexV2 = _vertices.IndexOf(edge.V2);

        if (indexV1 == -1 || indexV2 == -1)
            throw new ArgumentException($"V1 dan V2 tidak ada di vertices");

        _vertices[indexV1].AdjencyList.Add((_vertices[indexV2], edge.Weight));
        _vertices[indexV2].AdjencyList.Add((_vertices[indexV1], edge.Weight));
    }

    public bool RemoveVertex(Vertex<T> vertex)
    {
        var indeks = _vertices.FindIndex(x => x == vertex);

        if (indeks == -1) return false;

        _vertices.Remove(vertex);

        return true;
    }

    public bool RemoveEdge(Edge<T> edge)
    {
        if (!EdgesDistinct.Contains(edge)) return false;

        var indexV1 = _vertices.FindIndex(x => x == edge.V1);
        var indexV2 = _vertices.FindIndex(x => x == edge.V2);

        var indexAdjV1 = _vertices[indexV2].AdjencyList.FindIndex(a => a.adj == edge.V1);
        var indexAdjV2 = _vertices[indexV1].AdjencyList.FindIndex(a => a.adj == edge.V2);

        _vertices[indexV1].AdjencyList.RemoveAt(indexAdjV2);
        _vertices[indexV2].AdjencyList.RemoveAt(indexAdjV1);

        return true;
    }

    public List<Vertex<T>> Neighbor(Vertex<T> vertex)
    {
        var index = _vertices.FindIndex(v => v == vertex);
        if (index == -1) return [];
        return _vertices[index].AdjencyList.Select(adj => adj.adj).ToList();
    }

    public bool DetectCycleDFS(Vertex<T>? start = null)
    {
        if (start == null)
            start = Vertices[0];

        if (!Vertices.Contains(start))
            throw new ArgumentException("graph doesn't contain start vertex");

        var queue = new Queue<(Vertex<T> v, Vertex<T>? parent)>();
        var visited = new HashSet<Vertex<T>>();

        queue.Enqueue((start, null));

        while (queue.Count > 0)
        {
            var (v, parent) = queue.Dequeue();
            if (!visited.Add(v)) return true;

            var neighbor = Neighbor(v);

            foreach (var n in neighbor)
                if (n != parent)
                    queue.Enqueue((n, v));
        }

        return false;
    }

    public bool DetectCycleDUS()
    {
        var dus = new DisjointUnionSet<Vertex<T>>(_vertices);

        foreach (var e in EdgesDistinct)
            if (!dus.UnionByValue(e.V1, e.V2))
                return true;

        return false;
    }

    public void FruchtermanReingold(int width, int height, int iterations = 100)
    {
        var temperature = (float)width / 10;
        var area = width * height;
        var k = MathF.Sqrt(area / _vertices.Count);
        var random = new Random();

        float fa(float x) => (x * x) / k;
        float fr(float x) => (k * k) / (x + 0.0000001f);
        float cool(float t) => t - width / (10 * iterations);

        foreach (var e in _vertices)
        {
            e.Position = random.NextVector2(new Vector2(0, 0), new Vector2(width, height));
            e.Disposition = Vector2.Zero;
        }

        for (int i = 0; i < iterations; i++)
        {
            foreach (var v in _vertices)
            {
                v.Disposition = Vector2.Zero;
                foreach (var u in _vertices)
                {
                    if (u != v)
                    {
                        var delta = v.Position - u.Position;
                        v.Disposition = v.Disposition + (delta / (delta.Length() + (float)0.0000001)) * fr(delta.Length());
                    }
                }
            }

            foreach (var e in Edges)
            {
                var v1 = e.V1;
                var v2 = e.V2;
                var delta = v1.Position - v2.Position;
                v1.Disposition = v1.Disposition - (delta / (delta.Length() + (float)0.0000001)) * fa(delta.Length());
                v2.Disposition = v2.Disposition + (delta / (delta.Length() + (float)0.0000001)) * fa(delta.Length());
            }

            foreach (var v in _vertices)
            {
                v.Position =
                    v.Position +
                    (v.Disposition / (v.Disposition.Length() + (float)0.0000001)) *
                    MathF.Min(v.Disposition.Length(), temperature);

                v.Position = new Vector2(
                    MathF.Min(width, MathF.Max(0, v.Position.X)),
                    MathF.Min(height, MathF.Max(0, v.Position.Y)));
            }

            temperature = cool(temperature);
        }
    }

    public int Degree(Vertex<T> vertex)
    {
        var index = _vertices.IndexOf(vertex);

        if (index == -1)
            throw new ArgumentException("");

        return _vertices[index].AdjencyList.Count;
    }

    public (Graph<T> subgraph, List<(Graph<T> g, Edge<T>? bestEdge)> history) Kruskal()
    {
        var subgraph = new Graph<T>(_vertices.Select(v => new Vertex<T>(v.Value, v.Position, Vector2.Zero, [])).ToList(), []);

        var history = new List<(Graph<T> g, Edge<T>? bestEdge)> { (new(subgraph), null) };
        var priorityQueue = new MinHeapQueue<Edge<T>, double>(e => e.Weight, EdgesDistinct); 
        var dus = new DisjointUnionSet<Vertex<T>>(_vertices);

        while (subgraph.EdgesDistinct.Count < subgraph.Vertices.Count - 1)
        {
            var bestEdge = priorityQueue.Dequeue();

            while (subgraph.EdgesDistinct.Contains(bestEdge) || dus.FindByValue(bestEdge.V1) == dus.FindByValue(bestEdge.V2))
                bestEdge = priorityQueue.Dequeue();

            subgraph.AddEdge(bestEdge);
            dus.UnionByValue(bestEdge.V1, bestEdge.V2);
            history.Add((new Graph<T>(subgraph), bestEdge));
        }

        return (subgraph, history);
    }

    private class DjikstraNode(Vertex<T> vertex, double cost, Graph<T>.DjikstraNode? parent)
    {
        public Vertex<T> Vertex { get; set; } = vertex;
        public double Cost { get; set; } = cost;
        public DjikstraNode? Parent { get; set; } = parent;

        public override int GetHashCode() => Vertex.GetHashCode();
    }

    public List<(Vertex<T> end, double cost, List<Vertex<T>> path)> Djikstra(Vertex<T> start)
    {
        if (!_vertices.Contains(start))
            throw new ArgumentException("start tidak dalam graph");

        start = _vertices.Find(v => v == start)!;
        var startNode = new DjikstraNode(start, 0, null);

        var finalized = new HashSet<DjikstraNode>();
        var priorityQueue = new MinHeapQueue<DjikstraNode, double>(n => n.Cost);

        priorityQueue.Enqueue(startNode);

        while(priorityQueue.Count > 0)
        {
            var node = priorityQueue.Dequeue();
            finalized.Add(node);

            foreach (var (adj, weight) in node.Vertex.AdjencyList)
            {
                if(finalized.FirstOrDefault(n => n.Vertex == adj) == null)
                {
                    var inQueue = priorityQueue.Find(n => n.Vertex == adj);

                    if (inQueue == null)
                    {
                        priorityQueue.Enqueue(new(adj, node.Cost + weight, node));
                    } 
                    else
                    {
                        var newCost = node.Cost + weight;
                        if(newCost < inQueue.Cost)
                        {
                            inQueue.Cost = newCost;
                            inQueue.Parent = node;
                        }
                    }
                }
            }
        }

        var result = new List<(Vertex<T> end, double cost, List<Vertex<T>> path)>();

        foreach (var node in finalized)
        {
            var path = new List<Vertex<T>>() { node.Vertex };
            var parent = node.Parent;

            while(parent != null)
            {
                path.Add(parent.Vertex);
                parent = parent.Parent;
            }

            path.Reverse();
            result.Add((node.Vertex, node.Cost, path));
        }

        return result;
    }
}

public static class Graph
{
    public static Graph<int> CompleteGraph(int numOfVertex)
    {
        if (numOfVertex <= 0)
            throw new ArgumentException("numOfVertex is zero or negative");

        var random = new Random();
        var vertices = Enumerable.Range(1, numOfVertex).ToList();
        var edges = new List<(int, int, double)>();

        foreach (var v in vertices)
            foreach (var u in vertices)
                if (u != v)
                {
                    edges.Add((v, u, random.Next(1, 50)));
                }

        return new Graph<int>(vertices, edges);
    }

    public static Graph<int> RandomGraph(int numOfVertex, int degree)
    {
        if (numOfVertex <= 0)
            throw new ArgumentException("numOfVertex is zero or negative");

        if (degree <= 0)
            throw new ArgumentException("degree is zero or negative");

        degree = Math.Min(degree, numOfVertex - 1);

        var random = new Random();
        var vertices = Enumerable.Range(1, numOfVertex).ToList();
        var graph = new Graph<int>(vertices, []);
        var edgesAdded = 0;

        for (int i = 0; i < numOfVertex; i++)
        {
            graph.AddEdge(new Edge<int>(
                graph.Vertices[i], 
                graph.Vertices[i == numOfVertex - 1 ? 0 : i + 1], 
                random.Next(1, 50)));

            edgesAdded++;
        }

        var additionalEdge = Math.Min(edgesAdded + degree * (degree - 1)/2, numOfVertex * (numOfVertex - 1) / 2);
        while(edgesAdded < additionalEdge)
        {
            var i = random.Next(0, numOfVertex);
            var j = random.Next(0, numOfVertex);

            var v1 = graph.Vertices[i];
            var v2 = graph.Vertices[j];

            if(v1 != v2)
            {
                var edge = new Edge<int>(v1, v2, random.Next(1, 50));
                if(!graph.Edges.Contains(edge))
                {
                    graph.AddEdge(edge);
                    edgesAdded++;
                }
            }
        }


        return graph;
    }
}