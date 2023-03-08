using Camping.Entities.Classes;
using Camping.Entities.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Username for database access
        /// </summary>
        private readonly string _username;

        /// <summary>
        /// Password for database access
        /// </summary>
        private readonly string _password;

        /// <summary>
        /// Database handler
        /// </summary>
        private readonly Repository.Repository _repository;

        /// <summary>
        /// Bookings list
        /// </summary>
        private List<Booking>? _bookings;

        /// <summary>
        /// Customer list
        /// </summary>
        private List<Customer>? _customers;

        /// <summary>
        /// Pitch list
        /// </summary>
        private List<Pitch>? _pitches;

        /// <summary>
        /// Makes a new CampingWindow
        /// </summary>
        /// <param name="username">Username for database access</param>
        /// <param name="password">Password for database access</param>
        public CampingWindow(string username, string password)
        {
            // Set fields
            _username = username;
            _password = password;
            _repository = new(username, password);

            // Initialize
            InitializeComponent();
        }

        /// <summary>
        /// Called when initialized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Initialized(object sender, System.EventArgs e)
        {
            // Get data from database
            _bookings = _repository.GetList(_username, _password, new Booking());
            _customers = _repository.GetList(_username, _password, new Customer());
            _pitches = _repository.GetList(_username, _password, new Pitch());

            // Set ItemsSource
            DgdBookings.ItemsSource = new BookingWrapper(_bookings, _customers, _pitches).Bookings;
            CmxPitches.ItemsSource = _pitches.Select(p => p.PitchName);
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

        /// <summary>
        /// Handles BtnSearch on click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            // local vars
            var from = DateTime.MinValue;
            var to = DateTime.MinValue;

            // Parse text to DateTime
            try
            {
                from = DateTime.Parse(DprSearchFrom.Text);
                to = DateTime.Parse(DprSearchTo.Text);
            } 
            catch { }

            // If null return
            if (_pitches is null)
                return;

            // If try catch failed return
            if (from == DateTime.MinValue || to == DateTime.MinValue) 
                return;

            // Filter list
            var results = _pitches.Where(p =>
            {
                // If null return false
                if (_bookings is null)
                    return false;

                // Loop over _bookings
                foreach (var booking in _bookings)
                    if (booking.PitchId == p.PitchId) // If id matches
                        if (
                            (booking.StartDate > from && booking.StartDate < to) ||
                            (booking.EndDate < to && booking.EndDate > from) ||
                            (booking.StartDate < from && booking.EndDate > to)
                        ) // Return false if booking overlaps with the search
                            return false;

                // Return true by default
                return true;
            });

            // Convert results into a list of strings and set ItemsSource to the list
            LbxAvailable.ItemsSource = results.Select(p => p.PitchName);
        }
    }
}
