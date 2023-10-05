using CRUD_Workout;
using CRUD_Workout.Repositories;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CRUD_Workout.Repositories
{
    internal class WorkoutRepository : IWorkoutRepository
    {
        private static string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "databaseWorkouts.db");
        private static string _connectionStringWithDatabase = $"Data Source={dbFilePath}";

        private void CreateDatabase()
        {
            if (!File.Exists(dbFilePath))
            {
                using (SqliteConnection dbConnection = new SqliteConnection(_connectionStringWithDatabase))
                {
                    dbConnection.Open();

                    if (dbConnection.State == ConnectionState.Open)
                    {
                        string dbQuery = "CREATE TABLE IF NOT EXISTS Workouts(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Duration TEXT, Description TEXT, Image BLOB)";
                        SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                        dbCommand.ExecuteNonQuery();
                    }

                    dbConnection.Close();
                }
            }
        }

        private bool IsDatabaseExist()
        {
            bool isDatabaseExist = false;

            if (File.Exists(dbFilePath))
            {
                using (SqliteConnection dbConnection = new SqliteConnection(_connectionStringWithDatabase))
                {
                    dbConnection.Open();

                    if (dbConnection.State == ConnectionState.Open)
                    {
                        string dbQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Workouts'";
                        SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                        object? resultObj = dbCommand.ExecuteScalar();
                        string? result = resultObj as string;

                        if (result == "Workouts")
                            isDatabaseExist = true;
                    }

                    dbConnection.Close();
                }
            }

            return isDatabaseExist;
        }

        public WorkoutRepository()
        {
            if (!IsDatabaseExist())
                CreateDatabase();
        }

        public bool Create(Workout entity)
        {
            bool isCreated = false;

            using (SqliteConnection dbConnection = new SqliteConnection(_connectionStringWithDatabase))
            {
                dbConnection.Open();

                if (dbConnection.State == ConnectionState.Open)
                {
                    string dbQuery = $"SELECT COUNT(Id) FROM Workouts WHERE Name = '{entity.Name}'";
                    SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                    int result = Convert.ToInt32(dbCommand.ExecuteScalar());

                    if (result == 0)
                    {
                        dbQuery = $"INSERT INTO Workouts(Name, Duration, Description, Image) VALUES('{entity.Name}', '{entity.Duration}', '{entity.Description}', @Image)";
                        dbCommand.CommandText = dbQuery;
                        if (entity.Image != null)
                            dbCommand.Parameters.Add("@Image", SqliteType.Blob, entity.Image.Length).Value = entity.Image;
                        else
                        {
                            dbCommand.Parameters.AddWithValue("@Image", SqliteType.Blob).Value = new byte[0];
                        }
                        if (dbCommand.ExecuteNonQuery() == 1)
                            isCreated = true;
                    }
                }

                dbConnection.Close();
            }

            return isCreated;
        }

        public List<Workout> ReadAll()
        {
            List<Workout> workoutList = new List<Workout>();

            using (SqliteConnection dbConnection = new SqliteConnection(_connectionStringWithDatabase))
            {
                dbConnection.Open();

                if (dbConnection.State == ConnectionState.Open)
                {
                    string dbQuery = "SELECT * FROM Workouts";
                    SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                    SqliteDataReader dbDataReader = dbCommand.ExecuteReader();

                    while (dbDataReader.Read())
                    {
                        Workout workout = new Workout()
                        {
                            Id = Convert.ToInt32(dbDataReader["Id"]),
                            Name = dbDataReader["Name"].ToString(),
                            Duration = dbDataReader["Duration"].ToString(),
                            Description = dbDataReader["Description"].ToString(),
                            Image = dbDataReader["Image"] == DBNull.Value ? new byte[0] : (byte[])dbDataReader["Image"]
                        };

                        workoutList.Add(workout);
                    }
                }

                dbConnection.Close();
            }

            return workoutList;
        }

        public Workout Read(int Id)
        {
            Workout workout = new Workout();

            using (SqliteConnection dbConnection = new SqliteConnection(_connectionStringWithDatabase))
            {
                dbConnection.Open();

                if (dbConnection.State == ConnectionState.Open)
                {
                    string dbQuery = $"SELECT * FROM Workouts WHERE Id = {Id}";
                    SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                    SqliteDataReader dbDataReader = dbCommand.ExecuteReader();

                    while (dbDataReader.Read())
                    {
                        workout = new Workout()
                        {
                            Id = Convert.ToInt32(dbDataReader["Id"]),
                            Name = dbDataReader["Name"].ToString(),
                            Duration = dbDataReader["Duration"].ToString(),
                            Description = dbDataReader["Description"].ToString(),
                            Image = dbDataReader["Image"] == DBNull.Value ? new byte[0] : (byte[])dbDataReader["Image"]
                        };
                    }
                }

                dbConnection.Close();
            }

            return workout;
        }


        public bool Update(Workout entity)
        {
            bool isUpdated = false;

            using (SqliteConnection dbConnection = new SqliteConnection(_connectionStringWithDatabase))
            {
                dbConnection.Open();

                if (dbConnection.State == ConnectionState.Open)
                {
                    string dbQuery = $"SELECT COUNT(Id) FROM Workouts WHERE Name = '{entity.Name}'";
                    SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                    int result = Convert.ToInt32(dbCommand.ExecuteScalar());

                    if (result == 1)
                    {
                        dbQuery = $"UPDATE Workouts SET Duration = '{entity.Duration}', Description = '{entity.Description}', Image = @Image WHERE Id = {entity.Id}";
                        dbCommand.CommandText = dbQuery;
                        dbCommand.Parameters.Add("@Image", SqliteType.Blob, entity.Image.Length).Value = entity.Image;

                        if (dbCommand.ExecuteNonQuery() == 1)
                            isUpdated = true;
                    }
                }

                dbConnection.Close();
            }

            return isUpdated;
        }

        public bool Delete(Workout entity)
        {
            bool isDeleted = false;

            using (SqliteConnection dbConnection = new SqliteConnection(_connectionStringWithDatabase))
            {
                dbConnection.Open();

                if (dbConnection.State == ConnectionState.Open)
                {
                    string dbQuery = $"SELECT COUNT(Id) FROM Workouts WHERE Name = '{entity.Name}'";
                    SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);
                    int result = Convert.ToInt32(dbCommand.ExecuteScalar());

                    if (result == 1)
                    {
                        dbQuery = $"DELETE FROM Workouts WHERE Id = {entity.Id}";
                        dbCommand.CommandText = dbQuery;

                        if (dbCommand.ExecuteNonQuery() == 1)
                            isDeleted = true;
                    }
                }

                dbConnection.Close();
            }

            return isDeleted;
        }
    }
}