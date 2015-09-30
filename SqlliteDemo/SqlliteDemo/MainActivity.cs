using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;

namespace SqlliteDemo
{
	[Activity (Label = "SqlliteDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button> (Resource.Id.save);
            Button showdata = FindViewById<Button>(Resource.Id.showdata);
            var firstName = FindViewById<EditText> (Resource.Id.fistName);
			var lastName = FindViewById<EditText> (Resource.Id.lastName);

			button.Click += delegate {
             
              
                Connection cn = new Connection();
                cn.createDB(folder);
                Person p = new Person();
                p.FirstName = firstName.Text;
                p.LastName = lastName.Text;
                cn.insertUpdateData(p, folder);




            };
            showdata.Click += delegate
            {

                var intent = new Intent(this, typeof(ViewListActivity));
                //intent.PutStringArrayListExtra("personLists", listPer.GetAll(db));
                StartActivity(intent);
            };
        }

       
	}
}


