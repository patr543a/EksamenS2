using System.Windows;

namespace Camping
{
    /// <summary>
    /// Interaction logic for CampingWindow.xaml
    /// </summary>
    public partial class CampingWindow : Window
    {
        /// <summary>
        /// Weather window
        /// </summary>
        private WeatherWindow? _weatherWindow;

        /// <summary>
        /// Makes a new CampingWindow
        /// </summary>
        /// <param name="username">Username for database access</param>
        /// <param name="password">Password for database access</param>
        public CampingWindow(string username, string password)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open weather window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWeather_Click(object sender, RoutedEventArgs e)
        {
            _weatherWindow = new();
            _weatherWindow.Show();
        }
    }
}
