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
    public class PersonListView
    {
        public List<Person> GetAll(SQLiteConnection db) {
            var iqr = db.Query<Person>("select * from Person");
            return iqr.ToList();
        }
    }
}