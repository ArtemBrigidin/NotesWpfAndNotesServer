using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Text;
using Notes.Models;
using System.Net.Http;
using System;

namespace Notes
{
    public partial class Index : Window
    {
        User user;
        public Index(User _user)
        {
            InitializeComponent();
            user = _user;
            MessageBox.Show(user.Id.ToString());
        }

        private async void AddNoteBtn_Click(object sender, RoutedEventArgs e)
        {
            var Content = NoteContentField.Text;
            await SendData(user.Id.ToString(), Content);
        }

        async Task SendData(string Id, string Content)
        {
            var request = $"/addNote&{Id}&{Content}";
            byte[] requestData = Encoding.UTF8.GetBytes(request);
            var stream = user.connection.GetStream();
            await stream.WriteAsync(requestData);
            //byte[] requestData = Encoding.UTF8.GetBytes(request);
            //var stream = user.connection.GetStream();
            await stream.WriteAsync(requestData);
            byte[] responseData = new byte[1024];
            int bytes = 0;
            var response = new StringBuilder();
            bytes = await stream.ReadAsync(responseData);
            response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
            string s = response.ToString();
        }

        async Task EditNote(string _Note)
        {
            var request = $"/editNote&{user.Id}&{_Note}";
            byte[] requestData = Encoding.UTF8.GetBytes(request);
            var stream = user.connection.GetStream();
            await stream.WriteAsync(requestData);
        }

        async Task GetNotes(string Id)
        {
            var request = $"/getNotes&{Id}";
            byte[] requestData = Encoding.UTF8.GetBytes(request);
            var stream = user.connection.GetStream();
            await stream.WriteAsync(requestData);
            byte[] responseData = new byte[1024];
            int bytes = 0;
            var response = new StringBuilder();
            bytes = await stream.ReadAsync(responseData);
            response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
            //noteManager.GetNotes(responseData);
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();

        }
    }
}