using Camping.Services;
using System.Windows;

namespace Camping
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Camping window
        /// </summary>
        private CampingWindow? _campingWindow;

        /// <summary>
        /// Makes a new LoginWindow
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
        }

        // Closes LoginWindow
        private void BtnClose_Click(object sender, RoutedEventArgs e)
            => Close();

        /// <summary>
        /// Open CampingWindow if login was valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Check if login is valid
            var valid = LoginService.VerifyLogin(TbxUsername.Text, TbxPassword.Password);

            // if is valid then
            if (valid)
            {
                // Make new CampingWindow and show it
                _campingWindow = new(TbxUsername.Text, TbxPassword.Password);
                _campingWindow.Show();

                // Close this window and return
                Close();
                return;
            }

            // Show error if not valid
            MessageBox.Show("Ugyldig login!");
        }
    }
}
