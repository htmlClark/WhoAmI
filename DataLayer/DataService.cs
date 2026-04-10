using Core;
using Microsoft.Data.Sqlite;

namespace DataLayer
{
    public class DataService
    {
        List<Student> studentNames = new List<Student>
        {
            new Student
            {
                Name = "clark"
            },
            new Student
            {
                Name = "lily"
            }
        };
        
        public bool doesNameExist(string validName)
        {
            foreach (Student s in studentNames)
            {
                if(s.Name == validName)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Database
    {
        private string connectionString = "Data Source=app.db";

        public void Init()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = 
            @"CREATE TABLE IF NOT EXISTS Students(
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL);
            ";

            command.ExecuteNonQuery();
        }
    }
}


