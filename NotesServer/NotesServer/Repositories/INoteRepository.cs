using NotesServer.Models;

namespace NotesServer.Repositories
{
    internal interface INoteRepository
    {
        public void AddNote(Note note);
        public List<Note> GetNotes(string UserId);
        //void EditNote();
        //void DeleteEdit();
    }
}
