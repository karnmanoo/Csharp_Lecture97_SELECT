using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Csharp_Lecture97_SELECT
{
    class DataAccess
    {
        public const string dbpath = "Customers.db"; //ประกาศตัวแปรแทน Database ชื่อ Customers
        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Customers (uid INTEGER PRIMARY KEY, " +
                    "first_Name VARCHAR(255) NULL," +
                    "last_Name VARCHAR(255) NULL," +
                    "email VARCHAR(255) NULL)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }
        public static void AddData(string uid, string firstName, string lastName, string email)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Customers VALUES (@uid,@first_Name,@last_Name,@email);";
                insertCommand.Parameters.AddWithValue("@uid", uid);
                insertCommand.Parameters.AddWithValue("@first_Name", firstName);
                insertCommand.Parameters.AddWithValue("@last_Name", lastName);
                insertCommand.Parameters.AddWithValue("@email", email);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        public static List<String> GetData()
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT uid,first_Name,last_Name,email from Customers", db);
                //ถ้า SELECT ทุก Field ให้ใช้ SELECT * from ...
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                    entries.Add(query.GetString(1));
                    entries.Add(query.GetString(2));
                    entries.Add(query.GetString(3));
                }
                db.Close();
            }
            return entries;
        }
        public static void ClearData()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand clearCommand = new SqliteCommand();
                clearCommand.Connection = db;
                clearCommand.CommandText = "DELETE from Customers";
                clearCommand.ExecuteNonQuery();
                db.Close();
            }
        }
    }
}
