using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RoslynSamples
{
    public class WeatherForecastNPC : INotifyPropertyChanged
    {
        private DateTime _date;
        private int _temperatureCelsius;
        private string? _summary;

        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TemperatureCelsius
        {
            get => _temperatureCelsius;
            set
            {
                if (_temperatureCelsius != value)
                {                    
                    _temperatureCelsius = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Summary
        {
            get => _summary;
            set
            {
                if (_summary != value)
                {
                    _summary = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
