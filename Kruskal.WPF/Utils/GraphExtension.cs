using Kruskal.Core;
using System.Drawing;
using System.Numerics;

namespace Kruskal.WPF.Utils;

public static class GraphExtension
{
    public static Bitmap ToBitmap<T>(this Graph<T> graph, int width, int height, string title)
        where T : IEquatable<T>
    {
        var defaultPenColor = Color.Black;
        var defaultBrushColor = Color.Green;
        var bitmap = new Bitmap(width, height);
        using var g = Graphics.FromImage(bitmap);
        using var p = new Pen(defaultPenColor, 2);
        using var b = new SolidBrush(defaultBrushColor);
        using var f = new Font("Calibri", 14);

        var colors = new Random().RandomColors(graph.Edges.Count, [defaultPenColor, defaultBrushColor, Color.White]);

        var vertexSize = 23;

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
        for(int i = 0; i < graph.EdgesDistinct.Count; i++)
        {
            var e = graph.EdgesDistinct[i];
            var v1 = e.V1.Position;
            var v2 = e.V2.Position;
            var delta = (v1 - v2);

            var lineOffset = vertexSize / 2 / delta.Length();
            var lineXOffset = delta.X / MathF.Abs(delta.X + 1e-5f) * MathF.Abs(delta.X * lineOffset);
            var lineYOffset = delta.Y / MathF.Abs(delta.Y + 1e-5f) * MathF.Abs(delta.Y * lineOffset);

            p.Color = colors[i];
            g.DrawLine(
                p, 
                v1.X - lineXOffset, 
                v1.Y - lineYOffset, 
                v2.X + lineXOffset, 
                v2.Y + lineYOffset);

            p.Color = defaultPenColor;
        }

        //Draw Weight
        for (int i = 0; i < graph.EdgesDistinct.Count; i++)
        {
            var e = graph.EdgesDistinct[i];
            var v1 = e.V1.Position;
            var v2 = e.V2.Position;
            var delta = (v1 - v2);

            const float textOffset = 0.3f;
            var textXOffset = delta.X / MathF.Abs(delta.X + 1e-5f) * MathF.Abs(delta.X * textOffset);
            var textYOffset = delta.Y / MathF.Abs(delta.Y + 1e-5f) * MathF.Abs(delta.Y * textOffset);

            b.Color = Color.White;
            g.FillRectangle(
                b,
                v1.X - (textXOffset + 10),
                v1.Y - (textYOffset + 10),
                f.Size + 10, f.Size + 10);

            b.Color = colors[i];
            g.DrawString(
                e.Weight.ToString(), f, b,
                v1.X - (textXOffset + 10),
                v1.Y - (textYOffset + 10));

            b.Color = defaultBrushColor;
        }

        //Draw Title
        using var f1 = new Font("Calibri", 30);
        g.DrawString(title, f, b, 10, 10);

        return bitmap;
    }
}
