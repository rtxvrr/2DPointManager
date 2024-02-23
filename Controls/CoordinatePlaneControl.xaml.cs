using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace _2DPointManager.Controls
{
    /// <summary>
    /// Interaction logic for CoordinatePlaneControl.xaml
    /// </summary>
    public partial class CoordinatePlaneControl : UserControl
    {
        // Определение зависимых свойств для точек, координаты X и Y
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(ObservableCollection<Models.Point>), typeof(CoordinatePlaneControl));

        public static readonly DependencyProperty XCoordinateProperty =
            DependencyProperty.Register("XCoordinate", typeof(double), typeof(CoordinatePlaneControl));

        public static readonly DependencyProperty YCoordinateProperty =
            DependencyProperty.Register("YCoordinate", typeof(double), typeof(CoordinatePlaneControl));

        public ObservableCollection<Models.Point> Points
        {
            get { return (ObservableCollection<Models.Point>)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        public double XCoordinate
        {
            get { return (double)GetValue(XCoordinateProperty); }
            set { SetValue(XCoordinateProperty, value); }
        }

        public double YCoordinate
        {
            get { return (double)GetValue(YCoordinateProperty); }
            set { SetValue(YCoordinateProperty, value); }
        }

        public CoordinatePlaneControl()
        {
            InitializeComponent();
        }
    }
}
