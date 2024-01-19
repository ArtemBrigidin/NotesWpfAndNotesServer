using NotesServer.Models;

namespace NotesServer.Repositories
{
    internal interface IUserRepository
    {
        public string toHash(string password);
        public void Create(User user);
        public int Login(User user);
    }
}
