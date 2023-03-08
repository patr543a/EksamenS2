using Camping.Entities.Weather;
using Camping.Services;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Camping
{
    /// <summary>
    /// Interaction logic for WeatherWindow.xaml
    /// </summary>
    public partial class WeatherWindow : Window
    {
        private WeatherService _weatherService = new();
        private List<WeatherData>? _weatherData;

        /// <summary>
        /// Makes new WeatherWindow
        /// </summary>
        public WeatherWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when window is initialized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Initialized(object sender, EventArgs e)
        {
            // Get weather data
            _weatherData = _weatherService.GetWeather("Vejle", 2);

            // Check if data exist
            if (_weatherData is null || _weatherData.Count == 0)
                return;

            // Set DataGrid items to weather data
            DgdWeather.ItemsSource = _weatherData;

            // Set Location lable to location
            LblLocation.Content = $"Sted: {_weatherData[0].Location}";
        }
    }
}
