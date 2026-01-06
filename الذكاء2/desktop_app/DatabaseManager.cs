using System;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StudentPerformanceApp
{
    public static class DatabaseManager
    {
        private static readonly string ConnectionString = "Data Source=users.db;Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists("users.db"))
            {
                SQLiteConnection.CreateFile("users.db");
            }

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                // إنشاء جدول المستخدمين
                string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT UNIQUE NOT NULL,
                        Email TEXT UNIQUE NOT NULL,
                        Password TEXT NOT NULL,
                        FullName TEXT NOT NULL,
                        CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                    )";

                // إنشاء جدول التنبؤات
                string createPredictionsTable = @"
                    CREATE TABLE IF NOT EXISTS Predictions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER,
                        HoursStudied INTEGER,
                        PreviousScores INTEGER,
                        ExtracurricularActivities TEXT,
                        SleepHours INTEGER,
                        SamplePapers INTEGER,
                        PredictedPerformance REAL,
                        CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (UserId) REFERENCES Users (Id)
                    )";

                using (var command = new SQLiteCommand(createUsersTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SQLiteCommand(createPredictionsTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool RegisterUser(string username, string email, string password, string fullName)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string hashedPassword = HashPassword(password);

                    string query = @"
                        INSERT INTO Users (Username, Email, Password, FullName)
                        VALUES (@username, @email, @password, @fullName)";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", hashedPassword);
                        command.Parameters.AddWithValue("@fullName", fullName);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SQLiteException)
            {
                return false; // المستخدم موجود بالفعل
            }
        }

        public static User? LoginUser(string username, string password)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string hashedPassword = HashPassword(password);

                    string query = @"
                        SELECT Id, Username, Email, FullName
                        FROM Users
                        WHERE Username = @username AND Password = @password";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", hashedPassword);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    Id = reader.GetInt32("Id"),
                                    Username = reader.GetString("Username"),
                                    Email = reader.GetString("Email"),
                                    FullName = reader.GetString("FullName")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public static bool SavePrediction(int userId, int hoursStudied, int previousScores, 
            string extracurricular, int sleepHours, int samplePapers, double predictedPerformance)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Predictions 
                        (UserId, HoursStudied, PreviousScores, ExtracurricularActivities, 
                         SleepHours, SamplePapers, PredictedPerformance)
                        VALUES (@userId, @hoursStudied, @previousScores, @extracurricular, 
                                @sleepHours, @samplePapers, @predictedPerformance)";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@hoursStudied", hoursStudied);
                        command.Parameters.AddWithValue("@previousScores", previousScores);
                        command.Parameters.AddWithValue("@extracurricular", extracurricular);
                        command.Parameters.AddWithValue("@sleepHours", sleepHours);
                        command.Parameters.AddWithValue("@samplePapers", samplePapers);
                        command.Parameters.AddWithValue("@predictedPerformance", predictedPerformance);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string FullName { get; set; } = "";
    }
}