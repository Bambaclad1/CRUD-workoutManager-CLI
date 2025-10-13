using System.Text.Json;

namespace ConsoleApp1
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string? Summary { get; set; }
    }

    internal class JsonRepository
    {
        public static void DoThat()
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Parse("2025-10-13"),
                TemperatureCelsius = 16,
                Summary = "Breezy"
            };

            string fileName = "WeatherForecast.json";
            string jsonString = JsonSerializer.Serialize(weatherForecast);
            File.WriteAllText(fileName, jsonString);

            Console.WriteLine(jsonString);
        }
    }
}