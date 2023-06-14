using System.Text.Json;
using System.Text.Json.Serialization;

namespace RoslynSamples
{
    public static partial class CodeGeneratedJsonSerialization
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            TypeInfoResolver = SourceGenerationContext.Default
        };

        [JsonSourceGenerationOptions(WriteIndented = true)]
        [JsonSerializable(typeof(WeatherForecast))]
        internal partial class SourceGenerationContext : JsonSerializerContext
        {
        }

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
