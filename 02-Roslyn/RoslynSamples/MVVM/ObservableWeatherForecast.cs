using CommunityToolkit.Mvvm.ComponentModel;

namespace RoslynSamples.MVVM
{
    public partial class ObservableWeatherForecast : ObservableObject
    {
        [ObservableProperty]
        private DateTime _date;

        [ObservableProperty]
        private int _temperatureCelsius;

        [ObservableProperty]
        private string? _summary;
    }
}
