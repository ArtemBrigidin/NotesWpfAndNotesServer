using System.Threading.Tasks;
using System.Windows;
using System.Net.Sockets;
using System.Text;
using System;
using Notes.Models;

namespace Notes
{
    public partial class LoginForm : Window
    {
        static TcpClient tcpClient;

        public LoginForm()
        {
            InitializeComponent();
            Connection();
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Registration registration = new Registration(tcpClient);
            registration.Show();
        }

        static async Task Connection()
        {
            try
            {
                TcpClient _tcpClient = new TcpClient();
                await _tcpClient.ConnectAsync(Costants.IP, Costants.PORT);
                tcpClient = _tcpClient;
            }
            catch
            {
                MessageBox.Show("Не получилось :( ");
            }
        }

        private async void ClickButton_Login(object sender, RoutedEventArgs e)
        {
            var login = LoginField.Text;
            var password = PasswordField.Password;
            await SendData(login, password);

        }
        
        async Task SendData(string login, string password)
        {
            try
            {
                var request = $"/login&{login}&{password}";
                byte[] requestData = Encoding.UTF8.GetBytes(request);
                var stream = tcpClient.GetStream();
                await stream.WriteAsync(requestData);
                byte[] responseData = new byte[1024];
                int bytes = 0;
                var response = new StringBuilder();
                bytes = await stream.ReadAsync(responseData);
                response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
                string BuiderId = response.ToString();
                int Id = Convert.ToInt32(BuiderId);
                User newUser = new User(Id, tcpClient);
                if (Id > 0)
                {
                    Index index = new Index(newUser);
                    this.Close();
                    index.Show();
                    tcpClient.Close();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ReloadConnection(object sender, RoutedEventArgs e)
        {
            Connection();
        }
    }
}