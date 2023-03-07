using Camping.Services;

namespace Temp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WeatherService weatherService = new WeatherService();
            var w = weatherService.GetWeather("Vejle");
        }
    }
}