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

        public MainViewModel()
        {
            Points = new ObservableCollection<Models.Point>();
            AddPointCommand = new RelayCommand(AddPoint, null);
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
            if(xCoordinate >= -10 && xCoordinate <= 10 && yCoordinate >= -10 && yCoordinate <= 10)
                return true;
            return false;
        }

        private void AddPoint()
        {
            if (CanAddPoint(_xCoordinate, _yCoordinate))
            {
                Points.Add(new Models.Point(_xCoordinate, _yCoordinate));
                XCoordinate = 0;
                YCoordinate = 0;
            }
            else
            {
                MessageBox.Show("Диапозон координат для X и Y от -10 до 10");
                XCoordinate = 0;
                YCoordinate = 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
