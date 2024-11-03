using Kruskal.Core;

var vertices = new List<string>{ "A", "B", "C", "D", "E", "F", "G"};
var edges = new List<(string, string, int)>
{
    ("A", "B", 7),
    ("A", "D", 5),
    ("B", "C", 8),
    ("B", "D", 9),
    ("B", "E", 7),
    ("C", "E", 5),
    ("D", "E", 15),
    ("D", "F", 6),
    ("E", "F", 8),
    ("E", "G", 9),
    ("F", "G", 11)
};

var graph = new Graph<string>(vertices, edges);

foreach (var v in graph.Vertices)
    Console.WriteLine(v.Value);
foreach (var e in graph.Edges.Distinct())
    Console.WriteLine($"({e.V1.Value}, {e.V2.Value}; {e.Weight})");
Console.WriteLine($"Total Weight : {graph.TotalWeight}");

var (subgraph, history) = graph.Kruskal();

Console.WriteLine("Minimum Spanning Tree Kruskal");
foreach (var v in subgraph.Vertices)
    Console.WriteLine(v.Value);
foreach (var e in subgraph.Edges)
    Console.WriteLine($"({e.V1.Value}, {e.V2.Value}; {e.Weight})");
Console.WriteLine($"Total Weight : {subgraph.TotalWeight}");

graph.FruchtermanReingold(100, 100, 4);
foreach (var v in graph.Vertices)
{
    Console.WriteLine(v.Position);
}