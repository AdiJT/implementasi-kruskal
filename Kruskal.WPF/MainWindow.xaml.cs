using Kruskal.Core;
using Kruskal.WPF.Utils;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Btn_Execute_Click(object sender, RoutedEventArgs e)
        {
            var graph = Graph<int>.GenerateCompleteGraph(int.Parse(TextBox_NumOfVertex.Text));
            const int size = 400;
            graph.FruchtermanReingold(size, size, 10000);

            var (result, history) = graph.Kruskal();

            WrapPanel_Result.Children.Clear();
            WrapPanel_Result.Children.Add(new Image
            {
                Source = graph.ToBitmap(size, size).ToWpfBitmap(),
                Height = 400,
                Margin = new Thickness(10)
            });

            foreach (var (g, _) in history)
            {
                WrapPanel_Result.Children.Add(new Image
                {
                    Source = g.ToBitmap(size, size).ToWpfBitmap(),
                    Height = 400,
                    Margin = new Thickness(10)
                });
            }
        }
    }
}