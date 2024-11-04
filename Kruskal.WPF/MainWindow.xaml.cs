using Kruskal.Core;
using Kruskal.WPF.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kruskal.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Btn_Execute.Click += Btn_Execute_Click;
        }

        private async void Btn_Execute_Click(object sender, RoutedEventArgs e)
        {
            Progress_Bar.Value = 0;
            TextBox_NumOfVertex.IsEnabled = false;
            Btn_Execute.IsEnabled = false;
            IProgress<int> progress = new Progress<int>((x) => Progress_Bar.Value += x);

            var numOfVertex = (int)Math.Max(int.Parse(TextBox_NumOfVertex.Text), 2);

            var result = await Task.Run(() =>
            {
                //var graph = new Graph<int>(
                //    [0, 1, 2, 3, 4, 5, 6,],
                //    [
                //        (0, 1, 1),
                //        (0, 2, 8),
                //        (1, 4, 6),
                //        (1, 3, 3),
                //        (2, 4, 5),
                //        (3, 5, 4),
                //        (4, 5, 2),
                //        (4, 6, 8),
                //        (5, 6, 5),
                //        (5, 6, 7)
                //    ]
                //);

                var graph = Graph.RandomGraph(numOfVertex, 4);
                var size = 1000;
                graph.FruchtermanReingold(size, size);
                progress.Report(25);

                var (result, history) = graph.Kruskal();
                progress.Report(25);

                var bitmapSources = new List<BitmapSource> 
                { 
                    graph.ToBitmap(size, size, "Graph Asli").ToWpfBitmap() 
                };
                foreach (var (g, be) in history)
                {
                    bitmapSources.Add(g.ToBitmap(size, size, be is null ? "Iterasi 0" : $"Tambah Sisi ({be.V1.Value}, {be.V2.Value}) dengan bobot {be.Weight}").ToWpfBitmap());
                    progress.Report(50 * (1 / history.Count));
                }

                return (bitmapSources, graph, result);
            });

            WrapPanel_Result.Children.Clear();
            foreach (var bmp in result.bitmapSources)
            {
                Button element = new Button
                {
                    Content = new Image
                    {
                        Source = bmp,
                        Height = 400,
                    }
                };
                element.Click += (s, e) =>
                {
                    var window = new Window()
                    {
                        WindowState = WindowState.Maximized
                    };

                    var dp = new DockPanel()
                    {
                    };
                    dp.Children.Add(new Image() { Source = bmp, Stretch = Stretch.Uniform });

                    window.Content = dp;

                    window.Show();
                };

                WrapPanel_Result.Children.Add(element);
            }

            TextBox_TotalW.Text = result.graph.TotalWeight.ToString();
            TextBox_MSTW.Text = result.result.TotalWeight.ToString();

            TextBox_NumOfVertex.IsEnabled = true;
            Btn_Execute.IsEnabled = true;
            Progress_Bar.Value = 0;
        }
    }
}