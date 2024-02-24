using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace _2DPointManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private double _xCoordinate;
        private double _yCoordinate;
        private CoordinateControll _coordinateControll;

        public MainViewModel()
        {
            Points = new ObservableCollection<Models.Point>();
            AddPointCommand = new RelayCommand(AddPoint, null);
            try
            {
                _coordinateControll = new CoordinateControll();
                LoadPointsFromCoordinateControll();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при инициализации координатного контроллера: ", ex);
            }
        }

        public ObservableCollection<Models.Point> Points { get; }

        public double XCoordinate
        {
            get => _xCoordinate;
            set
            {
                _xCoordinate = value;
                OnPropertyChanged();
                (AddPointCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public double YCoordinate
        {
            get => _yCoordinate;
            set
            {
                _yCoordinate = value;
                OnPropertyChanged();
                (AddPointCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand AddPointCommand { get; }

        private bool CanAddPoint(double xCoordinate, double yCoordinate)
        {
            if (xCoordinate >= -10 && xCoordinate <= 10 && yCoordinate >= -10 && yCoordinate <= 10)
                return true;
            return false;
        }

        private async void AddPoint()
        {
            try
            {
                if (CanAddPoint(_xCoordinate, _yCoordinate))
                {
                    Points.Add(new Models.Point(_xCoordinate, _yCoordinate));
                    await _coordinateControll.UpdateCoordinatesAsync(_xCoordinate, _yCoordinate);
                    XCoordinate = 0;
                    YCoordinate = 0;
                }
                else
                {
                    MessageBox.Show("Диапазон координат для X и Y от -10 до 10");
                    XCoordinate = 0;
                    YCoordinate = 0;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при добавлении точки: ", ex);
            }
        }

        private void LoadPointsFromCoordinateControll()
        {
            try
            {
                foreach (var point in _coordinateControll.Points)
                {
                    Points.Add(point);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при загрузке точек из координатного контроллера: ", ex);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}. Пожалуйста, закройте приложение и попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
