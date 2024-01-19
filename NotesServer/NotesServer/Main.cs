using System.Net.Sockets;
using System.Text;
using NotesServer.Configuration;
using NotesServer.Repositories;
using NotesServer.Models;

var tcpListener = new TcpListener(ConfigurationData.IP, ConfigurationData.PORT);


IUserRepository userRepository;
UserRepository user = new UserRepository();
INoteRepository noteRepository;
NoteRepository note = new NoteRepository();

try
{
    tcpListener.Start();
    Console.WriteLine("Сервер запущен. Ожидание подключений... ");
    while (true)
    {
        using var tcpClient = await tcpListener.AcceptTcpClientAsync();
        var stream = tcpClient.GetStream();
        byte[] responseData = new byte[1024];
        int bytes = 0;
        var response = new StringBuilder();
        bytes = await stream.ReadAsync(responseData);
        response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
        string _response = response.ToString();
        string[] parts = _response.Split('&');
        string UserLogin = "";
        string UserPassword = "";

        switch (parts[0].ToString())
        {
            case "/login":
                Console.WriteLine("Login");
                UserLogin = parts[1];
                UserPassword = parts[2];
                var request = user.Login(new User() { Login = UserLogin, Password = UserPassword });
                if(request > 0) { 
                    var _request = request.ToString();
                    byte[] requestData = Encoding.UTF8.GetBytes(_request);
                    await stream.WriteAsync(requestData);
                }
                else
                {
                    string _request = "Crash";
                    byte[] requestData = Encoding.UTF8.GetBytes(_request);
                    await stream.WriteAsync(requestData);
                    continue;
                }
                break;
            case "/registration":
                Console.WriteLine("Registration");
                UserLogin = parts[1];
                UserPassword = parts[2];
                user.Create(new User() { Login = UserLogin, Password = UserPassword });
                break;
            case "/addNote":
                Console.WriteLine("AddNote");
                var UserId = parts[1];
                var Note = parts[2];
                note.AddNote(new Note { Content = Note, UserId = UserId });
                break;
            case "/editNote":
                Console.WriteLine("EditNote");
                UserId = parts[1];
                Note = parts[2];
                note.AddNote(new Note { Content = Note, UserId = UserId });
                break;
            case "/getNotes":
                UserId = parts[1];
                string? _requestNote = note.GetNotes(UserId).ToString();
                byte[] requestNote = Encoding.UTF8.GetBytes(_requestNote);
                await stream.WriteAsync(requestNote);
                break;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    tcpListener.Stop();
}