using System;
using System.Globalization;
using System.Windows.Data;

namespace _2DPointManager.ViewModels
{
    public class XCoordinateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double coordinate = System.Convert.ToDouble(value);
            double canvasSize = System.Convert.ToDouble(parameter);
            double scaledCoordinate = canvasSize / 2 + coordinate * 25; // Смещаем точку на половину размера холста и масштабируем по единице координаты = 25 пикселя
            return scaledCoordinate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class YCoordinateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double coordinate = System.Convert.ToDouble(value);
            double canvasSize = System.Convert.ToDouble(parameter);
            double scaledCoordinate = canvasSize / 2 - coordinate * 25; // Инвертируем координату Y и масштабируем по единице координаты = 25 пикселя
            return scaledCoordinate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
