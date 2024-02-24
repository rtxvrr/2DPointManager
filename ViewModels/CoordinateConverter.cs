using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace _2DPointManager.ViewModels
{
    public class XCoordinateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double coordinate = System.Convert.ToDouble(value);
                double canvasSize = System.Convert.ToDouble(parameter);
                double scaledCoordinate = canvasSize / 2 + coordinate * 25; // Смещаем точку на половину размера холста и масштабируем по единице координаты = 25 пикселя
                return scaledCoordinate;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при конвертации координаты X: ", ex);
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}. Пожалуйста, закройте приложение и попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public class YCoordinateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double coordinate = System.Convert.ToDouble(value);
                double canvasSize = System.Convert.ToDouble(parameter);
                double scaledCoordinate = canvasSize / 2 - coordinate * 25; // Инвертируем координату Y и масштабируем по единице координаты = 25 пикселя
                return scaledCoordinate;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при конвертации координаты Y: ", ex);
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}. Пожалуйста, закройте приложение и попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
