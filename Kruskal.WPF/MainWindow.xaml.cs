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
        private const int _size = 1000;
        private Graph<string>? _graph;
        private BitmapSource? _graphBmp;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddBitmapToWrapPanel(BitmapSource bmp)
        {
            var element = new Button
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

                var dp = new DockPanel();
                dp.Children.Add(new Image() { Source = bmp, Stretch = Stretch.Uniform });
                window.Content = dp;
                window.Show();
            };

            WrapPanel_Result.Children.Add(element);
        }

        private void Btn_Generate_Click(object sender, RoutedEventArgs e)
        {
            WrapPanel_Result.Children.Clear();

            var numOfVertex = int.Parse(TextBox_NumOfVertex.Text);
            var degree = int.Parse(TextBox_NumOfDegree.Text);
            var rGraph = Graph.RandomGraph(numOfVertex, degree);

            _graph = new Graph<string>(rGraph.Vertices.Select(v => new Vertex<string>(v.Value.ToString())).ToList(),
                rGraph.Edges.Select(e => new Edge<string>(new Vertex<string>(e.V1.Value.ToString()), new Vertex<string>(e.V2.Value.ToString()), e.Weight)).ToList());
            _graph.FruchtermanReingold(_size, _size);
            _graphBmp = _graph.ToBitmap(_size, _size, "Graph Asli").ToWpfBitmap();

            AddBitmapToWrapPanel(_graphBmp);
        }

        private async void Btn_Kruskal_Click(object sender, RoutedEventArgs e)
        {
            if(_graph is null || _graphBmp is null) return;

            Progress_Bar.Value = 0;
            WrapPanel_Result.Children.Clear();
            IsEnabled = false;

            IProgress<(int, BitmapSource?)> progress = new Progress<(int, BitmapSource?)>(
                (p) =>
                {
                    Progress_Bar.Value += p.Item1;
                    var bmp = p.Item2;

                    if (bmp is not null)
                    {
                        AddBitmapToWrapPanel(bmp);
                    }
                }
            );

            AddBitmapToWrapPanel(_graphBmp);

            var result = await Task.Run(() =>
            {
                var (subgraph, history) = _graph.Kruskal();

                foreach (var (g, be) in history)
                {
                    var bmp = g.ToBitmap(
                        _size, _size,
                        be is null ? "Iterasi 0" : $"Tambah Sisi ({be.V1.Value}, {be.V2.Value}) dengan bobot {be.Weight}")
                    .ToWpfBitmap();

                    progress.Report(((int)(100d * (1d / history.Count)), bmp));
                }

                return subgraph;
            });

            TextBox_TotalW.Text = _graph.TotalWeight.ToString();
            TextBox_MSTW.Text = result.TotalWeight.ToString();

            IsEnabled = true;
            Progress_Bar.Value = 0;
        }

        private async void Btn_Djikstra_Click(object sender, RoutedEventArgs e)
        {
            if (_graph is null || _graphBmp is null) return;

            Progress_Bar.Value = 0;
            WrapPanel_Result.Children.Clear();
            IsEnabled = false;

            IProgress<(int, BitmapSource?)> progress = new Progress<(int, BitmapSource?)>(
                (p) =>
                {
                    Progress_Bar.Value += p.Item1;
                    var bmp = p.Item2;

                    if (bmp is not null)
                    {
                        AddBitmapToWrapPanel(bmp);
                    }
                }
            );

            AddBitmapToWrapPanel(_graphBmp);

            var start = TextBox_Start.Text;

            await Task.Run(() =>
            {
                var result = _graph.Djikstra(new(start));

                foreach(var r in result)
                {
                    var bmp = _graph.ToBitmap(
                        _size, _size,
                        $"Shortest Path dari {start} ke {r.end.Value} dengan Cost {r.cost}",
                        r).ToWpfBitmap();

                    progress.Report(((int)(100d * (1d / result.Count)), bmp));
                }
            });

            Progress_Bar.Value = 0;
            IsEnabled = true;
        }

        private void Btn_Generate_Contoh_Click(object sender, RoutedEventArgs e)
        {
            _graph = new(["Kayu Putih", "TDM", "Oesapa Barat", "Oesapa", "Oesapa Selatan", "Liliba", "Lasiana", "Penfui"],
                new List<(string v1, string v2, double weight)>
                {
                    ("Kayu Putih", "Oesapa Barat", 3.9),
                    ("Kayu Putih", "TDM", 1.8),
                    ("Oesapa Barat", "Oesapa", 2.1),
                    ("Oesapa Barat", "TDM", 3.1),
                    ("TDM", "Liliba", 3.1),
                    ("Liliba", "Oesapa Selatan", 1.8),
                    ("Oesapa", "Oesapa Selatan", 2.2),
                    ("Oesapa", "Lasiana", 2.4),
                    ("Lasiana", "Penfui", 5.8),
                    ("Oesapa Selatan", "Penfui", 2.9)
                });
            _graph.FruchtermanReingold(_size, _size);
            _graphBmp = _graph.ToBitmap(_size, _size, "Graph Asli").ToWpfBitmap();

            AddBitmapToWrapPanel(_graphBmp);
        }

        private async void Btn_Djikstra2_Click(object sender, RoutedEventArgs e)
        {
            if (_graph is null || _graphBmp is null) return;

            Progress_Bar.Value = 0;
            WrapPanel_Result.Children.Clear();
            IsEnabled = false;

            IProgress<(int, BitmapSource?)> progress = new Progress<(int, BitmapSource?)>(
                (p) =>
                {
                    Progress_Bar.Value += p.Item1;
                    var bmp = p.Item2;

                    if (bmp is not null)
                    {
                        AddBitmapToWrapPanel(bmp);
                    }
                }
            );

            AddBitmapToWrapPanel(_graphBmp);

            var start = TextBox_Start.Text;
            var end = TextBox_End.Text;

            await Task.Run(() =>
            {
                var result = _graph.Djikstra(new(start), new(end));

                    var bmp = _graph.ToBitmap(
                        _size, _size,
                        $"Shortest Path dari {start} ke {end} dengan Cost {result.cost}",
                        (new(end), result.cost, result.path)).ToWpfBitmap();

                progress.Report((100, bmp));
            });

            Progress_Bar.Value = 0;
            IsEnabled = true;
        }
    }
}