using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Sample_App.Services
{
    public class DbHandler
    {
        static string connectionstring = string.Empty;
        private SqlCommand cmd = null;
        private string sql = null;

        static DbHandler()
        {
            connectionstring = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            ChecknCreateTable();
        }

        private static void ChecknCreateTable()
        {
            try
            {
                string theDate = "LoginTrack";

                // Enclose the connection inside a using statement to close and dispose
                // when you don't need anymore the connection (to free local and server resources)
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    // Sql command with parameter 
                    string cmdText = @"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES 
                           WHERE TABLE_NAME=@name) SELECT 1 ELSE SELECT 0";
                    connection.Open();
                    SqlCommand DateCheck = new SqlCommand(cmdText, connection);

                    // Add the parameter value to the command parameters collection
                    DateCheck.Parameters.Add("@name", SqlDbType.NVarChar).Value = theDate;

                    // IF EXISTS returns the SELECT 1 if the table exists or SELECT 0 if not
                    int x = Convert.ToInt32(DateCheck.ExecuteScalar());
                    if (x != 1)
                        CreateTable();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format("Check n Create Table failure: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
            }
        }

        private static void CreateTable()
        {
            try
            {
                Logger.WriteLog(string.Format("Connecting to: {0}", connectionstring));
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    // Adding records the table  
                    string sqltext = "CREATE TABLE LoginTrack (Id INT IDENTITY(1, 1) NOT NULL, Name TEXT NOT NULL, LoginDate DATETIME NOT NULL DEFAULT GETDATE())";
                    SqlCommand cmd1 = new SqlCommand(sqltext, connection);

                    int i = cmd1.ExecuteNonQuery();

                    Logger.WriteLog(string.Format("SQL Query execution successful."));

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format("Create Table failure: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
            }
        }

        public bool TestConnection()
        {
            bool isconnected = false;
            try
            {
                Logger.WriteLog(string.Format("Connecting to: {0}", connectionstring));
                using (var connection = new SqlConnection(connectionstring))
                {
                    var query = "select 1";
                    Logger.WriteLog(string.Format("Executing query : {0}", query));

                    var command = new SqlCommand(query, connection);

                    connection.Open();
                    Logger.WriteLog(string.Format("SQL Connection successful."));

                    int i = (int)command.ExecuteScalar();
                    Logger.WriteLog(string.Format("SQL Query execution successful."));
                    isconnected = (i == 1);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format("Test Connection failure: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
            }

            return isconnected;
        }

        public bool InsertUserInformation(string name)
        {
            bool isinsertDone = false;
            try
            {
                Logger.WriteLog(string.Format("Connecting to: {0}", connectionstring));
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    // Adding records the table  
                    sql = @"INSERT INTO LoginTrack(Name) VALUES (@name)";
                    cmd = new SqlCommand(sql, connection);

                    // Add the parameter value to the command parameters collection
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;

                    int i = cmd.ExecuteNonQuery();

                    Logger.WriteLog(string.Format("SQL Query execution successful."));
                    isinsertDone = (i == 1);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format("Insert User Information failure: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
            }

            return isinsertDone;
        }

        public DataSet ReadUserInformation()
        {
            DataSet ds = new DataSet();
            try
            {
                Logger.WriteLog(string.Format("Connecting to: {0}", connectionstring));
                using (var connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM LoginTrack", connection);
                    // Create DataSet, fill it and view in data grid                     
                    da.Fill(ds);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(string.Format("Read User Information failure: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
            }

            return ds;
        }
    }
}
