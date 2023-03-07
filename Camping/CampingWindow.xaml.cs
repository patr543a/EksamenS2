using System.Windows;

namespace Camping
{
    /// <summary>
    /// Interaction logic for CampingWindow.xaml
    /// </summary>
    public partial class CampingWindow : Window
    {
        private WeatherWindow? _weatherWindow;

        public CampingWindow(string username, string password)
        {
            InitializeComponent();
        }

        private void BtnWeather_Click(object sender, RoutedEventArgs e)
        {
            _weatherWindow = new();
            _weatherWindow.Show();
        }
    }
}
