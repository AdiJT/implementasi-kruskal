using Kruskal.Core;
using System.Drawing;
using System.Numerics;

namespace Kruskal.WPF.Utils;

public static class GraphExtension
{
    public static Bitmap ToBitmap<T>(this Graph<T> graph, int width, int height)
        where T : IEquatable<T>
    {
        var bitmap = new Bitmap(width, height);
        using var g = Graphics.FromImage(bitmap);
        using var p = new Pen(Color.Black, 1);
        using var b = new SolidBrush(Color.Green);
        using var f = new Font("Calibri", 10);

        var vertexSize = 12;

        g.Clear(Color.White);

        //Draw vertices
        foreach (var v in graph.Vertices)
        {
            var x = v.Position.X - vertexSize / 2;
            x = MathF.Max(x, 0);
            x = MathF.Min(x, width - vertexSize);

            var y = v.Position.Y - vertexSize / 2;
            y = MathF.Max(y, 0);
            y = MathF.Min(y, height - vertexSize);

            g.DrawEllipse(p, x, y, vertexSize, vertexSize);
            g.DrawString(v.Value.ToString(), f, b, x, y);
        }

        //Draw edges
        foreach (var e in graph.Edges)
        {
            var v1 = graph.Vertices.FirstOrDefault(x => x == e.V1)!;
            var v2 = graph.Vertices.FirstOrDefault(x => x == e.V2)!;
            g.DrawLine(p, v1.Position.X, v1.Position.Y, v2.Position.X, v2.Position.Y);
            var start = Vector2.Min(v1.Position, v2.Position);
            var end = Vector2.Max(v1.Position, v2.Position);
            var delta = end - start;
            g.DrawString(e.Weight.ToString(), f, b, start.X + delta.X / 2, start.Y + delta.Y / 2);
        }

        return bitmap;
    }
}
