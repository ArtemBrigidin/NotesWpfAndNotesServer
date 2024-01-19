using System.Net.Sockets;

namespace Notes.Models
{
    public class User
    {
        public int? Id;

        public TcpClient connection;

        public User(int _Id, TcpClient tcpClient)
        {
            Id = _Id;
            connection = tcpClient;
        }
    }
}
