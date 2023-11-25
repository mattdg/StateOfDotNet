using System.Text.Json;
using System.Text.Json.Serialization;

namespace RoslynSamples
{
    public static partial class CodeGeneratedJsonSerialization
    {
        [JsonSourceGenerationOptions(WriteIndented = true)]
        [JsonSerializable(typeof(WeatherForecast))]
        internal partial class SourceGenerationContext : JsonSerializerContext
        {
        }

        public static string Serialize(WeatherForecast forecast)
        {
            return JsonSerializer.Serialize(forecast, SourceGenerationContext.Default.WeatherForecast);
        }

        public static WeatherForecast? Deserialize(string forecast)
        {
            return JsonSerializer.Deserialize(forecast, SourceGenerationContext.Default.WeatherForecast);
        }
    }
}
