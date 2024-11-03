using System.Drawing;

namespace Kruskal.WPF.Utils
{
    public static class RandomExtension
    {
        public static List<Color> RandomColors(this Random random, int count, List<Color> excludes)
        {
            var colors = new List<Color>();

            for(int i = 0; i < count; i++)
            {
                var color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

                while(excludes.Contains(color) && colors.Contains(color))
                    color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

                colors.Add(color);
            }

            return colors;
        }
    }
}
