using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Notes
{
    public partial class Registration : Window
    {
        TcpClient tcpClient;
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

        private void ClickButton_Registration(object sender, RoutedEventArgs e)
        {
            var login = LoginField.Text;
            var password = PasswordField.Password;
            SendData(login, password);
        }

        async Task SendData(string login, string password)
        {
            var request = $"/registration&{login}&{password}";
            byte[] requestData = Encoding.UTF8.GetBytes(request);
            var stream = tcpClient.GetStream();
            await stream.WriteAsync(requestData);
            byte[] responseData = new byte[1024];
            int bytes = 0;
            var response = new StringBuilder();
            bytes = await stream.ReadAsync(responseData);
            response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
            string s = response.ToString();
        }
    }
}
