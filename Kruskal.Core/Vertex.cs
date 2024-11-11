using System.Numerics;

namespace Kruskal.Core;

public class Vertex<T> : IEquatable<Vertex<T>>
    where T : IEquatable<T>
{
    public T Value { get; set; }
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Disposition { get; set; } = Vector2.Zero;
    public List<(Vertex<T> adj, double weight)> AdjencyList { get; } = [];

    public Vertex(T value)
    {
        Value = value;
    }

    public Vertex(T value, Vector2 position, Vector2 disposition, List<(Vertex<T> adj, double Weight)> adjencyList)
    {
        Value = value;
        Position = position;
        Disposition = disposition;
        AdjencyList = adjencyList;
    }

    public override bool Equals(object? obj) => obj is not null && obj is Vertex<T> vrt && vrt.Value.Equals(Value);

    public bool Equals(Vertex<T>? other) => other is not null && other.Value.Equals(Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Vertex<T>? left, Vertex<T>? right) => left is not null && left.Equals(right);

    public static bool operator !=(Vertex<T>? left, Vertex<T>? right) => !(left == right);
}