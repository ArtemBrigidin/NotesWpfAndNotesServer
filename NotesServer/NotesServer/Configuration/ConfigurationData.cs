using System.Net;

namespace NotesServer.Configuration
{
    internal class ConfigurationData
    {
        public const int PORT = 35353;
        public static IPAddress IP = IPAddress.Parse("26.212.217.210");
        public const string ConnectionString = "Server=ADMIN\\SQLEXPRESS;Database=Notes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";
    }
}
