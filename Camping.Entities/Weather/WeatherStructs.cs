using Newtonsoft.Json;

namespace Camping.Entities.Weather
{
    // Structs used for representing Api json responds
    public struct Root
    {
        [JsonProperty("list")]
        public List<List> List;

        [JsonProperty("city")]
        public City City;
    }

    public struct City
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("country")]
        public string Country;
    }

    public struct List
    {
        [JsonProperty("dt")]
        public int Dt;

        [JsonProperty("main")]
        public Main Main;

        [JsonProperty("weather")]
        public List<Weather> Weather;
    }

    public struct Main
    {
        [JsonProperty("temp")]
        public double Temp;
    }

    public struct Weather
    {
        [JsonProperty("description")]
        public string Description;

        [JsonProperty("icon")]
        public string Icon;
    }
}
