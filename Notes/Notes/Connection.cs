using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Notes
{
    public class Connection
    {
        public const int PORT = 35353;
        public static IPAddress IP = IPAddress.Parse("26.212.217.210");
        public static TcpClient tcpClient;

        public int UserId { get; set; }

        public async Task SendDataOfUser(string request)
        {
            if (request.StartsWith("/login"))
            {
                try
                {
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
                    if (Id > 0)
                    {
                        UserId = Id;
                    }
                }
                catch
                {
                    MessageBox.Show("Авторизоваться не вышло :(");
                }
            }
            if (request.StartsWith("/registration"))
            {
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

        public async Task GetDataOfRequestOfUser()
        {
            byte[] responseData = new byte[1024];
            int bytes = 0;
            var stream = tcpClient.GetStream();
            var response = new StringBuilder();
            bytes = await stream.ReadAsync(responseData);
            response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
            string BuiderId = response.ToString();
            int Id = Convert.ToInt32(BuiderId);
            if (Id > 0)
            {
                UserId = Convert.ToInt32(Id);
            }
        }

        public static async Task GetConnection()
        {
            try
            {
                TcpClient _tcpClient = new TcpClient();
                await _tcpClient.ConnectAsync(IP, PORT);
                tcpClient = _tcpClient;
            }
            catch
            {
                MessageBox.Show("Не получилось :( Мб сервер выключен");
            }
        }
    }
}
