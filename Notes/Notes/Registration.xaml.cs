using System.Net.Sockets;
using System.Windows;

namespace Notes
{
    public partial class Registration : Window
    {
        TcpClient tcpClient;
        Connection conn = new Connection();
        public Registration(TcpClient _tcpClient)
        {
            InitializeComponent();
            tcpClient = _tcpClient;
        }

        private void GoLogin(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private async void ClickButton_Registration(object sender, RoutedEventArgs e)
        {
            var login = LoginField.Text;
            var password = PasswordField.Password;
            await conn.SendDataOfUser($"/registration&{login}&{password}");
        }
    }
}
