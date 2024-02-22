using _2DPointManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2DPointManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private double _xCoordinate;
        private double _yCoordinate;

        public MainViewModel()
        {
            Points = new ObservableCollection<Point>();
            AddPointCommand = new RelayCommand(AddPoint, CanAddPoint);
        }

        public ObservableCollection<Point> Points { get; }

        public double XCoordinate
        {
            get => _xCoordinate;
            set
            {
                _xCoordinate = value;
                OnPropertyChanged();
            }
        }

        public double YCoordinate
        {
            get => _yCoordinate;
            set
            {
                _yCoordinate = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddPointCommand { get; }

        private bool CanAddPoint() => _xCoordinate >= -10 && _xCoordinate <= 10 &&
                                       _yCoordinate >= -10 && _yCoordinate <= 10;

        private void AddPoint()
        {
            Points.Add(new Point(_xCoordinate, _yCoordinate));
            XCoordinate = 0;
            YCoordinate = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
