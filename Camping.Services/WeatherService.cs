using Newtonsoft.Json;
using System.Drawing;
using System.Net;

namespace Camping.Services
{
    public class WeatherService
    {
        private static readonly string url = @"https://api.openweathermap.org/data/2.5/weather";

        public static Dictionary<string, (double, double)> Locations { get; } = new()
        {
            { "Standard", (44.34, 10.99) },
            { "Vejle", (55.71015526921322, 9.522407129627638) },
        };

        public WeatherRoot GetWeather(string location)
        {
            WeatherRoot w = new();

            using (WebClient client = new())
            {
                (double Lat, double Lon) position = Locations["Standard"];

                if (Locations.TryGetValue(location, out (double, double) value))
                    position = value;

                string json = client.DownloadString($"{url}?lat={position.Lat}&lon={position.Lon}&units=metric&appid=10c644d196c062655691095f2eadf73b");

                w = JsonConvert.DeserializeObject<WeatherRoot>(json)! ?? w;
            }

            return w;
        }

        public class WeatherRoot
        {

        }
    }
}
