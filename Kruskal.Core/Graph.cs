using System.Drawing;
using System.Numerics;

namespace Kruskal.Core;

public class Graph<T> where T : IEquatable<T>
{
    private readonly List<List<(Vertex<T> v, int weight)>> _adjencyList;
    private readonly List<Vertex<T>> _vertices;

    public IReadOnlyList<Vertex<T>> Vertices => _vertices;
    public IReadOnlyList<IReadOnlyList<(Vertex<T> v, int weight)>> AdjencyList => _adjencyList;

    public IReadOnlyList<Edge<T>> Edges 
    {
        get
        {
            var edges = _vertices.SelectMany((v, i) => _adjencyList[i].Select(a => new Edge<T>(v, a.v, a.weight)))
                .ToList()
                .Distinct()
                .ToList();

            var result = new List<Edge<T>>();

            foreach ( var edge in edges)
            {
                if (!result.Contains(edge))
                {
                    result.Add(edge);
                }
            }

            return result;
        }
    }

    public int TotalWeight => Edges.Aggregate(0, (acc, e) => acc + e.Weight);

    public Graph(List<T> vertices, List<(T v1, T v2, int weight)> edges)
    {
        foreach (var (v1, v2, weight) in edges)
        {
            if (!vertices.Contains(v1) || !vertices.Contains(v2))
                throw new ArgumentException("Ada edge dengan verteks yang tidak ada di 'verteks'");

            if (v1.Equals(v2))
                throw new ArgumentException("Terdapat loop");

            if (weight <= 0)
                throw new ArgumentException("Ada edge dengan weight kurang dari 0");
        }

        _vertices = vertices.Distinct().Select(t => new Vertex<T>(t)).ToList();
        _adjencyList = _vertices.Select(x => new List<(Vertex<T> v, int weight)>()).ToList();

        foreach (var (v1, v2, weight) in edges)
        {
            var indeksV1 = vertices.FindIndex(t => t.Equals(v1));
            var indeksV2 = vertices.FindIndex(t => t.Equals(v2));

            if (_adjencyList[indeksV1].FindIndex(x => x.v.Value.Equals(v2)) == -1)
            {
                _adjencyList[indeksV1].Add(((Vertex<T> v, int weight))(new Vertex<T>(v2), weight));
            }

            if (_adjencyList[indeksV2].FindIndex(x => x.v.Value.Equals(v1)) == -1)
            {
                _adjencyList[indeksV2].Add(((Vertex<T> v, int weight))(new Vertex<T>(v1), weight));
            }
        }
    }

    public Graph(List<Vertex<T>> vertices, List<Edge<T>> edges)
    {
        _vertices = vertices.Distinct().ToList();
        _adjencyList = vertices.Select(v => new List<(Vertex<T> v, int weight)>()).ToList();

        foreach (var edge in edges)
        {
            var indexV1 = _vertices.FindIndex(x => x == edge.V1);
            var indexV2 = _vertices.FindIndex(x => x == edge.V2);

            if (indexV1 == -1 || indexV2 == -1)
                throw new ArgumentException($"no vertex {edge.V1} or {edge.V2} in vertices");

            if (edge.Weight < 0)
                throw new ArgumentException("negatif edge weight");

            if (_adjencyList[indexV1].FindIndex(adj => adj.v == edge.V2) == -1)
                _adjencyList[indexV1].Add((edge.V2, edge.Weight));

            if (_adjencyList[indexV2].FindIndex(adj => adj.v == edge.V1) == -1)
                _adjencyList[indexV2].Add((edge.V1, edge.Weight));
        }
    }

    public Graph(Graph<T> graph)
    {
        _vertices = graph._vertices.Select(v => new Vertex<T>(v.Value, v.Position, v.Disposition)).ToList();
        _adjencyList = graph._adjencyList.Select(adj => adj.Select(a => (new Vertex<T>(a.v.Value), a.weight)).ToList()).ToList();
    }

    public int AddVertex(Vertex<T> v)
    {
        if (_vertices.Contains(v))
            return _vertices.FindIndex(x => x == v);

        _vertices.Add(v);
        _adjencyList.Add([]);
        return _vertices.Count - 1;
    }

    public void AddEdge(Edge<T> edge)
    {
        if (Edges.Contains(edge))
            throw new ArgumentException($"Sudah ada edge antara {edge.V1.Value} dan {edge.V2.Value}");

        if (edge.V1 == edge.V2)
            throw new ArgumentException($"Sisi adalah loop pada {edge.V1.Value}");

        if (edge.Weight < 0)
            throw new ArgumentException("weight negatif");

        var indexV1 = AddVertex(edge.V1);
        var indexV2 = AddVertex(edge.V2);

        _adjencyList[indexV1].Add((edge.V2, edge.Weight));
        _adjencyList[indexV2].Add((edge.V1, edge.Weight));
    }

    public bool RemoveVertex(Vertex<T> vertex)
    {
        var indeks = _vertices.FindIndex(x => x == vertex);

        if (indeks == -1) return false;

        _vertices.Remove(vertex);
        _adjencyList.RemoveAt(indeks);

        return true;
    }

    public bool RemoveEdge(Edge<T> edge)
    {
        if (!Edges.Contains(edge)) return false;

        var indexV1 = _vertices.FindIndex(x => x == edge.V1);
        var indexV2 = _vertices.FindIndex(x => x == edge.V2);

        var indexAdjV1 = _adjencyList[indexV2].FindIndex(a => a.v == edge.V1);
        var indexAdjV2 = _adjencyList[indexV1].FindIndex(a => a.v == edge.V2);

        _adjencyList[indexV1].RemoveAt(indexAdjV2);
        _adjencyList[indexV2].RemoveAt(indexAdjV1);

        return true;
    }

    public List<Vertex<T>> Neighbor(Vertex<T> vertex)
    {
        var index = _vertices.FindIndex(v => v == vertex);

        if (index == -1) return [];

        return _adjencyList[index].Select(adj => adj.v).ToList();
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

        foreach (var e in Edges)
        {
            if (!dus.UnionByValue(e.V1, e.V2))
                return true;
        }

        return false;
    }

    public bool DetectCycleIfAddDUS(Edge<T> newEdge)
    {
        if (!_vertices.Contains(newEdge.V1) || !_vertices.Contains(newEdge.V2))
            throw new ArgumentOutOfRangeException("newEdge is invalid");

        var dus = new DisjointUnionSet<Vertex<T>>(_vertices);
        var edges = Edges.Concat([newEdge]).ToList();

        foreach (var e in edges)
        {
            if (!dus.UnionByValue(e.V1, e.V2))
                return true;
        }

        return false;
    }

    public void FruchtermanReingold(int width, int height, int iterations = 100)
    {
        var halfWidth = width / 2;
        int halfHeight = height / 2;
        var temperature = (float)width/10;
        var area = width * height;
        var k = MathF.Sqrt(area / _vertices.Count);
        var random = new Random();

        float fa(float x) => (x * x) / k;
        float fr(float x) => (k * k) / x;
        float cool(float t) => t - width/100;
        
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
                        v.Disposition = v.Disposition + (delta / delta.Length()) * fr(delta.Length());
                    }
                }
            }

            foreach (var e in Edges)
            {
                var delta = e.V1.Position - e.V2.Position;
                e.V1.Disposition = e.V1.Disposition - (delta / delta.Length()) * fa(delta.Length());
                e.V2.Disposition = e.V2.Disposition + (delta / delta.Length()) * fa(delta.Length());
            }

            foreach (var v in _vertices)
            {
                v.Position = v.Position + (v.Disposition / v.Disposition.Length()) * MathF.Min(v.Disposition.Length(), temperature);
                v.Position = new Vector2(
                    MathF.Min(halfWidth, MathF.Max(-halfWidth, v.Position.X)),
                    MathF.Min(halfHeight, MathF.Max(-halfHeight, v.Position.Y)));
            }

            temperature = cool(temperature);
        }
    }

    public (Graph<T> subgraph, List<(Graph<T> g, Edge<T>? bestEdge)> history) Kruskal()
    {
        var subgraph = new Graph<T>(_vertices, []);
        var history = new List<(Graph<T> g, Edge<T>? bestEdge)> { (new(subgraph), null) };

        while(subgraph.Edges.Count < _vertices.Count - 1)
        {
            Edge<T>? bestEdge = null;

            foreach (var edge in Edges)
            {
                if(!subgraph.Edges.Contains(edge) && !subgraph.DetectCycleIfAddDUS(edge))
                {
                    if(bestEdge is null)
                        bestEdge = edge;
                    else
                    {
                        if (edge.Weight < bestEdge.Weight)
                            bestEdge = edge;
                    }
                }
            }
            subgraph.AddEdge(bestEdge!);

            history.Add((new Graph<T>(subgraph), bestEdge));
        }

        return (subgraph, history);
    }
}

public class Edge<T> : IEquatable<Edge<T>>
    where T : IEquatable<T>
{
    public Vertex<T> V1 { get; set; }
    public Vertex<T> V2 { get; set; }
    public int Weight { get; set; }

    public Edge(Vertex<T> v1, Vertex<T> v2, int weight)
    {
        V1 = v1;
        V2 = v2;
        Weight = weight;
    }

    public Edge(T v1, T v2, int weight)
    {
        V1 = new Vertex<T>(v1);
        V2 = new Vertex<T>(v2);
        Weight = weight;
    }

    public override bool Equals(object? obj) =>
        obj is not null &&
        obj is Edge<T> other &&
        ((other.V1 == V1 && other.V2 == V2) ||
        (other.V1 == V2 && other.V2 == V1));

    public bool Equals(Edge<T>? other) =>
        other is not null &&
        ((other.V1 == V1 && other.V2 == V2) ||
        (other.V1 == V2 && other.V2 == V1));

    public override int GetHashCode() => HashCode.Combine(V1, V2);

    public static bool operator ==(Edge<T>? left, Edge<T>? right) => left is not null && left.Equals(right);

    public static bool operator !=(Edge<T>? left, Edge<T>? right) => !(left == right);
}

public class Vertex<T> : IEquatable<Vertex<T>>
    where T : IEquatable<T>
{
    public T Value { get; set; }
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Disposition { get; set; } = Vector2.Zero;

    public Vertex(T value)
    {
        Value = value;
    }

    public Vertex(T value, Vector2 position, Vector2 disposition)
    {
        Value = value;
        Position = position;
        Disposition = disposition;
    }

    public override bool Equals(object? obj) => obj is not null && obj is Vertex<T> vrt && vrt.Value.Equals(Value);

    public bool Equals(Vertex<T>? other) => other is not null && other.Value.Equals(Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Vertex<T>? left, Vertex<T>? right) => left is not null && left.Equals(right);

    public static bool operator !=(Vertex<T>? left, Vertex<T>? right) => !(left == right);
}