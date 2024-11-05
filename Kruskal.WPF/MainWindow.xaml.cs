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
        }

        private async void Btn_Execute_Click(object sender, RoutedEventArgs e)
        {
            Progress_Bar.Value = 0;
            WrapPanel_Result.Children.Clear();
            TextBox_NumOfVertex.IsEnabled = false;
            TextBox_NumOfDegree.IsEnabled = false;
            Btn_Execute.IsEnabled = false;

            IProgress<(int, BitmapSource?)> progress = new Progress<(int, BitmapSource?)>(
                (p) =>
                {
                    Progress_Bar.Value += p.Item1;
                    var bmp = p.Item2;

                    if (bmp is not null)
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
                }
            );

            var numOfVertex = Math.Max(int.Parse(TextBox_NumOfVertex.Text), 2);
            var numOfDegree = int.Parse(TextBox_NumOfDegree.Text);

            var result = await Task.Run(() =>
            {
                var graph = Graph.RandomGraph(numOfVertex, numOfDegree);
                var size = 1000;
                graph.FruchtermanReingold(size, size);
                progress.Report((25, graph.ToBitmap(size, size, "Graph Asli").ToWpfBitmap()));

                var (subgraph, history) = graph.Kruskal();
                progress.Report((25, null));

                foreach (var (g, be) in history)
                {
                    var bmp = g.ToBitmap(
                        size, size,
                        be is null ? "Iterasi 0" : $"Tambah Sisi ({be.V1.Value}, {be.V2.Value}) dengan bobot {be.Weight}")
                    .ToWpfBitmap();

                    progress.Report((50 * (1 / history.Count), bmp));
                }

                return (graph, subgraph);
            });

            TextBox_TotalW.Text = result.graph.TotalWeight.ToString();
            TextBox_MSTW.Text = result.subgraph.TotalWeight.ToString();

            TextBox_NumOfVertex.IsEnabled = true;
            TextBox_NumOfDegree.IsEnabled = true;
            Btn_Execute.IsEnabled = true;
            Progress_Bar.Value = 0;
        }
    }
}