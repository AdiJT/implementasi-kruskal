namespace Kruskal.Core;

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
