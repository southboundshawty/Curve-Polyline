using System.Windows;
using System.Windows.Media;

namespace Curve
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var t = 40;

            var a = new PointCollection
            {
                new Point(3 * t, 1.5 * t),
                new Point(2 * t, 1 * t),
                new Point(3 * t, 2 * t),
                new Point(3 * t, 3 * t),
                new Point(4 * t, 2.5 * t),
                new Point(4.8 * t, 3.7 * t),
                new Point(3 * t, 5.5 * t),
                new Point(6 * t, 8 * t),
                new Point(7 * t, 9.5 * t),
                new Point(8.3 * t, 5.1 * t),
                new Point(6.5 * t, 4.2 * t),
                new Point(7 * t, 3 * t),
                new Point(8 * t, 2 * t),
                new Point(9 * t, 2 * t),
                new Point(9 * t, 3 * t)
            };

            Mem.Points = a;
            Mem2.Points = a;
        }
    }
}
