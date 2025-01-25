﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Shopping.DAL
{
    public class ShoppingDAL
    {
        /*-------------------------------Get all data-------------------------------*/
        public DataSet GetDataSet()
        {
            var ds = new DataSet("DataBase");
            var queries = new (string query, string tableName)[]
            {
                ("SELECT * FROM Users", "Users"),
                ("SELECT * FROM Products", "Products"),
                ("SELECT * FROM Orders", "Orders")
            };
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ChoiceCon"].ConnectionString))
                {
                    foreach (var (query, tableName) in queries)
                    {
                        using (var da = new SqlDataAdapter(query, conn))
                        {
                            da.Fill(ds, tableName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return ds;
        }

        /*-------------------------------Regiter new user-------------------------------*/

        public bool AddNewUser(DataSet ds)
        {
            DataTable usersTable = ds.Tables["Users"];
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ChoiceCon"].ConnectionString))
            {
                using (var da = new SqlDataAdapter("SELECT * FROM Users", conn))
                {
                    var commandBuilder = new SqlCommandBuilder(da);
                    try
                    {
                        da.Update(usersTable);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding new user: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        /*-------------------------------Update db after checkout-------------------------------*/

        public void UpdateDB(DataSet ds)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ChoiceCon"].ConnectionString))
                {
                    foreach (DataTable table in ds.Tables)
                    {
                        using (var da = new SqlDataAdapter($"SELECT * FROM {table.TableName}", conn))
                        {
                            var commandBuilder = new SqlCommandBuilder(da);
                            da.Update(ds, table.TableName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the database: {ex.Message}");
            }
        }
    }
}