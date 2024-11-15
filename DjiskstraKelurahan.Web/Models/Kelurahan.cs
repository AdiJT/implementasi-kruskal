namespace DjiskstraKelurahan.Web.Models;

public class Kelurahan : IEquatable<Kelurahan>
{
    public required string Nama { get; set; }
    public required string PlaceId { get; set; }

    public bool Equals(Kelurahan? other) => other is not null && other.Nama == Nama;

    public override bool Equals(object? obj) => obj is not null && obj is Kelurahan other && other.Nama == Nama;

    public override int GetHashCode() => Nama.GetHashCode();
}
