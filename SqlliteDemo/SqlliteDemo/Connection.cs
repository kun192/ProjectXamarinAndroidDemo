using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace SqlliteDemo
{
    public class Connection
    {
        public string createDB(string path)
        {
            try
            {
           
                var connection = new SQLiteAsyncConnection(System.IO.Path.Combine(path, "SQLiteTest.db"));
                connection.CreateTableAsync<Person>().ContinueWith(t => {
                    Console.WriteLine("Table created!");
                });
                return "Table created!";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public string insertUpdateData(Person data, string folder)
        {
            try
            {
                var db = new SQLiteConnection(System.IO.Path.Combine(folder, "SQLiteTest.db"));
                int iCOunt = db.Insert(data);

               return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }
    }
}