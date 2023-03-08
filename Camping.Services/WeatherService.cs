using Camping.Entities.Weather;
using Newtonsoft.Json;
using System.Net;

namespace Camping.Services
{
    /// <summary>
    /// Handles Api calls to openweathermap.org
    /// </summary>
    public class WeatherService
    {
        /// <summary>
        /// Url for Api
        /// </summary>
        private readonly string _baseUrl = string.Empty;

        /// <summary>
        /// Dictionary of location ids
        /// </summary>
        private static Dictionary<string, int> _locations { get; } = new()
        {
            { "Vejle", 2610613 },
        };

        /// <summary>
        /// Gets the list of location ids
        /// </summary>
        public static Dictionary<string, int> Locations => _locations;

        /// <summary>
        /// Makes a new WeatherService with a default baseUrl of https://api.openweathermap.org/data/2.5/
        /// </summary>
        public WeatherService() 
            : this(@"https://api.openweathermap.org/data/2.5/") { }

        /// <summary>
        /// Creates a new WeatherService with the given baseUrl
        /// </summary>
        /// <param name="baseUrl">Url to use to access Api</param>
        public WeatherService(string baseUrl)
            => _baseUrl = baseUrl;

        /// <summary>
        /// Gets the weather from the Api
        /// </summary>
        /// <param name="location">Location name to get the weather from</param>
        /// <param name="daysAhead">How many days to get forcasts for. Max 5 days</param>
        /// <returns>List of 3 hours forcast</returns>
        public List<WeatherData> GetWeather(string location, int daysAhead)
        {
            // Weather list
            var weather = new List<WeatherData>();
            // Get id of the location
            var id = GetCityId(location);

            // Get Json responds from url
            var json = CallWebApi($"{_baseUrl}forecast?id={id}&lang=da&units=metric&appid=10c644d196c062655691095f2eadf73b");

            // Convert responds to Root class
            var r = JsonConvert.DeserializeObject<Root>(json ?? "");

            // Loop over list in Root
            foreach (var l in r.List)
                weather.Add(new WeatherData( // Make new WeatherData and add to the list
                    new DateTime(l.Dt),
                    $"{r.City.Name}, {r.City.Country}",
                    l.Main.Temp.ToString("0.##"),
                    l.Weather[0].Description,
                    l.Weather[0].Icon
                ));

            // Return list
            return weather;
        }

        /// <summary>
        /// Gets the id of a city
        /// </summary>
        /// <param name="location">Location to get the id of</param>
        /// <returns>Returns the id of the location. If location was not found the id of Vejle is returned</returns>
        private static int GetCityId(string location)
            => _locations.TryGetValue(location, out int value) ? value : 2610613;

        /// <summary>
        /// Calls the Api and get a Json responds
        /// </summary>
        /// <param name="url">Url to get responds from</param>
        /// <returns>Json string of the responds from the server</returns>
        private static string? CallWebApi(string url)
        {
            using WebClient client = new();
            return client.DownloadString(url);
        }
    }
}
