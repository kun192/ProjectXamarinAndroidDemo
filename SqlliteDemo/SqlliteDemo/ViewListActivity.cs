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
    [Activity(Label = "ViewListActivity")]
    public class ViewListActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.layout1);
            //var listView = FindViewById<ListView>(Resource.Id.listView1);
            Button goBack = FindViewById<Button>(Resource.Id.goback);
            ListView mylistView = FindViewById<ListView>(Resource.Id.mylistView);
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var db = new SQLiteConnection(System.IO.Path.Combine(folder, "SQLiteTest.db"));
            PersonListView list = new PersonListView();
            list.GetAll(db);
            mylistView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemChecked, list.GetAll(db).ToArray());

            mylistView.SetItemChecked(1, true);

            goBack.Click += delegate
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
                // Create your application here


            }

        //protected override void OnListItemClick(ListView l, View v, int position, long id)
        //{
        //    l.SetItemChecked(1,true);
        //}
    }
}