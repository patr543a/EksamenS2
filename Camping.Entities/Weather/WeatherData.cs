namespace Camping.Entities.Weather
{
    /// <summary>
    /// Represents Weather data
    /// </summary>
    public struct WeatherData
    {
        /// <summary>
        /// Date and time of forecast
        /// </summary>
        private DateTime _date;

        /// <summary>
        /// Location of forecast
        /// </summary>
        private string _location = string.Empty;

        /// <summary>
        /// Temperature at _date
        /// </summary>
        private string _degrees = string.Empty;

        /// <summary>
        /// Description of the weather at _date
        /// </summary>
        private string _description = string.Empty;

        /// <summary>
        /// Icon of the weather at _date
        /// </summary>
        private string _icon = string.Empty;

        /// <summary>
        /// Gets or sets the date and time of forecast
        /// </summary>
        public DateTime Date { get => _date; set => _date = value; }

        /// <summary>
        /// Gets or sets the location of forecast
        /// </summary>
        public string Location { get => _location; set => _location = value; }

        /// <summary>
        /// Gets or sets the temperature at Date
        /// </summary>
        public string Degrees { get => $"{_degrees} C°"; set => _degrees = value; }

        /// <summary>
        /// Gets or sets the description of the weather at Date
        /// </summary>
        public string Description { get => _description; set => _description = value; }

        /// <summary>
        /// Gets or sets the icon of the Weather at Date
        /// </summary>
        public string Icon { get => _icon; set => _icon = value; }

        /// <summary>
        /// Creates a new WeatherData
        /// </summary>
        /// <param name="date">Date of the forecast</param>
        /// <param name="location">Location of the forecast</param>
        /// <param name="degrees">The temperature of the forecast</param>
        /// <param name="description">The description of the forecast</param>
        /// <param name="icon">The icon of the forecast</param>
        public WeatherData(DateTime date, string location, string degrees, string description, string icon)
        {
            Date = date;
            Location = location;
            Degrees = degrees;
            Description = description;
            Icon = icon;
        }
    }
}
