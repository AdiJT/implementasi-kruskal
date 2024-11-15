using Kruskal.Core;

namespace DjiskstraKelurahan.Web.Models.Home;

public class IndexVM
{
    public string? Start { get; set; }
    public string? End { get; set; }

    public Kelurahan? Origin { get; set; }
    public Kelurahan? Destinations { get; set; }
    public (double cost, List<Vertex<Kelurahan>> path)? Path { get; set; }
}
