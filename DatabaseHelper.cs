using System;
using System.Data;
using System.Data.SqlClient;

namespace TrainingApp
{
    public static class DatabaseHelper
    {
        private static string connectionString = "data source=DESKTOP-N9AD6FJ;initial catalog=TrainingDB;integrated security=True"; // Укажите строку подключения к вашей базе данных

        // Метод для инициализации базы данных (например, создание таблиц)
        public static void InitializeDatabase()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Пример создания таблиц, если они еще не существуют
                string createUsersTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
                    CREATE TABLE Users (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(100),
                        Age INT,
                        Weight FLOAT,
                        Gender NVARCHAR(10)
                    );
                ";

                string createWorkoutsTable = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Workouts' AND xtype='U')
                    CREATE TABLE Workouts (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        UserId INT,
                        Goal NVARCHAR(100),
                        Recommendation NVARCHAR(255),
                        FOREIGN KEY (UserId) REFERENCES Users(Id)
                    );
                ";

                using (var command = new SqlCommand(createUsersTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqlCommand(createWorkoutsTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для добавления пользователя
        public static int AddUser(string name, int age, double weight, string gender)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Users (Name, Age, Weight, Gender)
                    VALUES (@Name, @Age, @Weight, @Gender);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);"; // Возвращает ID добавленного пользователя

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@Weight", weight);
                    command.Parameters.AddWithValue("@Gender", gender);

                    return (int)command.ExecuteScalar(); // Возвращает ID
                }
            }
        }

        // Метод для добавления тренировки
        public static void AddWorkout(int userId, string goal, string recommendation)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Workouts (UserId, Goal, Recommendation)
                    VALUES (@UserId, @Goal, @Recommendation);";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Goal", goal);
                    command.Parameters.AddWithValue("@Recommendation", recommendation);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для получения всех тренировок
        public static DataTable GetAllWorkouts()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Workouts";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }
    }
}
