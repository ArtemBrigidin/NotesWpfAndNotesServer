using System.Net;

namespace NotesServer.Configuration
{
    internal class ConfigurationData
    {
        public const int PORT = 35353;
        public static IPAddress IP = IPAddress.Parse("26.212.217.210");
        public const string ConnectionString = "@\"Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=Notes;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False\"";
    }
}
