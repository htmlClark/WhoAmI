using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Core;
using Microsoft.Data.Sqlite;

namespace DataLayer
{
    public class DataService
    {
        private Database database = new Database();
        public void AddProfile(string validProfile)
        {
            var connection = database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Students (Name) VALUES (@validProfile)";
            command.Parameters.AddWithValue(@"validProfile", validProfile);

            command.ExecuteNonQuery();
        }
        public bool DoesStudentExist(string nameToLook)
        {
            var connection = database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM Students WHERE Name = @nameToLook";
            command.Parameters.AddWithValue(@"nameToLook", nameToLook);
            
            long count = (long)command.ExecuteScalar();

            return count > 0;
        }
        public void UpdateStudent(int studentId, string newName)
        {
            var connection = database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE Students SET Name = @newName WHERE Id = @studentId";
            command.Parameters.AddWithValue("@newName", newName);
            command.Parameters.AddWithValue("@studentId", studentId);

            command.ExecuteNonQuery();
        }
        public void DeleteStudent(int studentId, string StudentName)
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
        private string connectionString = "Data Source=whoAmI.db";
        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }
        public bool InitDb()
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = 
                @"CREATE TABLE IF NOT EXISTS Students(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL UNIQUE);
                ";
                command.ExecuteNonQuery();

                command.CommandText = @"INSERT OR IGNORE INTO Students (Name) VALUES ('lily');";
                command.ExecuteNonQuery();

                command.CommandText = @"INSERT OR IGNORE INTO Students (Name) VALUES ('clark');";
                command.ExecuteNonQuery();

                return true; 
                
            } catch
            {
                return false;
            }
            
        }
    }
}


