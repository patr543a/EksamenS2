using Newtonsoft.Json;
using System.Net;

namespace Camping.Services
{
    public class WeatherService
    {
        private readonly string _baseUrl = string.Empty;

        private static Dictionary<string, int> _locations { get; } = new()
        {
            { "Vejle", 2610613 },
        };

        public static Dictionary<string, int> Locations => _locations;

        public WeatherService() 
            : this(@"https://api.openweathermap.org/data/2.5/") { }

        public WeatherService(string baseUrl)
            => _baseUrl = baseUrl;

        public List<WeatherData> GetWeather(string location)
        {
            var weather = new List<WeatherData>();

            using (WebClient client = new())
            {
                var id = GetCityId(location);

                var json = CallWebApi($"{_baseUrl}forecast?id={id}&lang=da&units=metric&appid=10c644d196c062655691095f2eadf73b");

                var r = JsonConvert.DeserializeObject<Root>(json);

                foreach (var l in r.List)
                {
                    weather.Add(new WeatherData(
                        new DateTime(l.Dt),
                        $"{r.City.Name}, {r.City.Country}",
                        l.Main.Temp.ToString("0.##"),
                        l.Weather[0].Description,
                        l.Weather[0].Icon
                    ));
                }
            }

            return weather;
        }

        private static int GetCityId(string location)
            => _locations.TryGetValue(location, out int value) ? value : 2610613;

        private static string? CallWebApi(string url)
        {
            using WebClient client = new();
            return client.DownloadString(url);
        }
    }

    public struct WeatherData
    {
        private DateTime _date;
        private string _location = string.Empty;
        private string _degrees = string.Empty;
        private string _description = string.Empty;
        private string _icon = string.Empty;

        public DateTime Date { get => _date; set => _date = value; }
        public string Location { get => _location; set => _location = value; }
        public string Degrees { get => $"{_degrees} C°"; set => _degrees = value; }
        public string Description { get => _description; set => _description = value; }
        public string Icon { get => _icon; set => _icon = value; }

        public WeatherData(DateTime date, string location, string degrees, string description, string icon)
        {
            Date = date;
            Location = location;
            Degrees = degrees;
            Description = description;
            Icon = icon;
        }
    }

    internal struct Root
    {
        [JsonProperty("list")]
        public List<List> List;

        [JsonProperty("city")]
        public City City;
    }

    internal struct City
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("country")]
        public string Country;
    }

    internal struct List
    {
        [JsonProperty("dt")]
        public int Dt;

        [JsonProperty("main")]
        public Main Main;

        [JsonProperty("weather")]
        public List<Weather> Weather;
    }

    internal struct Main
    {
        [JsonProperty("temp")]
        public double Temp;
    }

    internal struct Weather
    {
        [JsonProperty("description")]
        public string Description;

        [JsonProperty("icon")]
        public string Icon;
    }
}
