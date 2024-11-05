using Kruskal.Core;
using System.Drawing;
using System.Numerics;

namespace Kruskal.WPF.Utils;

public static class GraphExtension
{
    public static Bitmap ToBitmap<T>(this Graph<T> graph, int width, int height, string title, int margin = 50)
        where T : IEquatable<T>
    {
        var defaultPenColor = Color.Black;
        var defaultBrushColor = Color.Green;
        var bitmap = new Bitmap(width + 2 * margin, height + 2 * margin);
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

            g.DrawEllipse(p, x + margin, y + margin, vertexSize, vertexSize);
            g.DrawString(v.Value.ToString(), f, b, x + margin, y + margin);
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
                v1.X - lineXOffset + margin,
                v1.Y - lineYOffset + margin,
                v2.X + lineXOffset + margin,
                v2.Y + lineYOffset + margin);

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
                Math.Max(Math.Min(v1.X - (textXOffset + 10), width - f.Size), 0) + margin,
                Math.Max(Math.Min(v1.Y - (textYOffset + 10), height - f.Size), 0) + margin,
                f.Size + 10, f.Size + 10);

            b.Color = colors[i];
            g.DrawString(
                e.Weight.ToString(), f, b,
                Math.Max(Math.Min(v1.X - (textXOffset + 10), width - f.Size), 0) + margin,
                Math.Max(Math.Min(v1.Y - (textYOffset + 10), height - f.Size), 0) + margin);

            b.Color = defaultBrushColor;
        }

        //Draw Title
        using var f1 = new Font("Calibri", 30);
        g.DrawString(title, f, b, 10, 10);

        return bitmap;
    }
}
