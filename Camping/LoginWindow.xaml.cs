using Camping.Services;
using System.Windows;

namespace Camping
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private CampingWindow? _campingWindow;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
            => Close();

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var valid = LoginService.VerifyLogin(TbxUsername.Text, TbxPassword.Password);

            if (valid)
            {
                _campingWindow = new(TbxUsername.Text, TbxPassword.Password);
                _campingWindow.Show();

                Close();

                return;
            }

            MessageBox.Show("Ugyldig login!");
        }
    }
}
