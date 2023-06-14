namespace RoslynSamples.JsonSerialization
{
    public static class Demo
    {
        public static void Run()
        {
            var forecast = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureCelsius = 25,
                Summary = "Hot"
            };

            //var forecastAsString = TraditionalJsonSerialization.Serialize(forecast);
            //var deserializedForecast = TraditionalJsonSerialization.Deserialize(forecastAsString)!;

            var forecastAsString = CodeGeneratedJsonSerialization.Serialize(forecast);
            var deserializedForecast = CodeGeneratedJsonSerialization.Deserialize(forecastAsString)!;

            Console.WriteLine($"The forecast for {deserializedForecast.Date} is {deserializedForecast.TemperatureCelsius} degrees and {deserializedForecast.Summary}.");
        }
    }
}
