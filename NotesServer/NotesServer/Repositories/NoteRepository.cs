using Microsoft.Data.SqlClient;
using NotesServer.Models;
using System.Data;
using Dapper;
using NotesServer.Configuration;

namespace NotesServer.Repositories
{

    public class NoteRepository : INoteRepository
    {
        public void AddNote(Note note)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationData.ConnectionString))
            {
                var sqlQuery = "INSERT INTO [dbo].[Note] (Note, UserId) VALUES(@Content, @UserId)";
                db.Execute(sqlQuery, note);
            }
        }

        public List<Note> GetNotes(string UserId)
        {
            List<Note> notes = new List<Note>();

            using (SqlConnection connection = new SqlConnection(ConfigurationData.ConnectionString))
            {
                connection.Open();

                string query = "SELECT Note FROM Note WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Note note = new Note
                            {
                                Content = reader.GetString(0)
                            };

                            notes.Add(note);
                        }
                    }
                }
            }

            return notes;
        }

        //void EditNote()
        //{

        //}
        //void DeleteEdit()
        //{

        //}
    }
}
