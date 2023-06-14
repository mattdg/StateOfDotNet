using System.Text.Json;

namespace RoslynSamples
{
    public static class TraditionalJsonSerialization
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions();

        public static string Serialize(WeatherForecast forecast)
        {
            return JsonSerializer.Serialize(forecast, _options);
        }

        public static WeatherForecast? Deserialize(string forecast)
        {
            return JsonSerializer.Deserialize<WeatherForecast>(forecast, _options);
        }
    }
}
