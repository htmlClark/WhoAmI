using System.Data;
using System.Runtime.InteropServices;
using Core;
using Microsoft.Data.Sqlite;

namespace DataLayer
{
    public class DataService
    {
        private Database database = new Database();

        public void addStudent(string ValidName)
        {
            var connection = database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Students (Name) VALUES (@name)";
            command.Parameters.AddWithValue(@"Validname", ValidName);

            command.ExecuteNonQuery();
        }

        public bool doesNameExist(string nameToLook)
        {
            var connection = database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM Students WHERE Name = @nameToLook";
            command.Parameters.AddWithValue(@"nameToLook", nameToLook);
            
            long count = (long)command.ExecuteScalar();

            return count > 0;
        }

        public void updateStudent(int studentId, string newName)
        {
            var connection = database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE Students SET Name = @newName WHERE Id = @studentId";
            command.Parameters.AddWithValue("@newName", newName);
            command.Parameters.AddWithValue("@studentId", studentId);

            command.ExecuteNonQuery();
        }
        public void deleteStudent(int studentId, string StudentName)
        {
            var connection = database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Students WHERE Id = @studentId";
            command.Parameters.AddWithValue(@"studentId", studentId);

            command.ExecuteNonQuery();
        }
    }

    public class Database
    {
        private string connectionString = "Data Source=app.db";

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Init()
        {
            using var connection = GetConnection();
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


