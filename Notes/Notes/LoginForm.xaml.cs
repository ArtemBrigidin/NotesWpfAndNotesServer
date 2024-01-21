using System.Windows;
using System.Net.Sockets;

namespace Notes
{
    public partial class LoginForm : Window
    {
        static TcpClient tcpClient;

        Connection conn = new Connection();

        public LoginForm()
        {
            InitializeComponent();
            Connection.GetConnection();
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Registration registration = new Registration(tcpClient);
            registration.Show();
        }

        private async void ClickButton_Login(object sender, RoutedEventArgs e)
        {
            var login = LoginField.Text;
            var password = PasswordField.Password;
            await conn.SendDataOfUser($"/login&{login}&{password}");
            if (conn.UserId > 0)
            {
                Index index = new Index(conn);
                this.Hide();
                index.Show();
            }
        }
    }
}