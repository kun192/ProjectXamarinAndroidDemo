using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;

namespace gcmDemo
{
	[Activity (Label = "gcmDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		TextView msgText;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			msgText = FindViewById<TextView>(Resource.Id.msgText);

            if (IsPlayServicesAvailable()) {

                var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);
            }
		}

		public bool IsPlayServicesAvailable ()
		{
			int resultCode = GooglePlayServicesUtil.IsGooglePlayServicesAvailable (this);
			if (resultCode != ConnectionResult.Success)
			{
				if (GooglePlayServicesUtil.IsUserRecoverableError (resultCode))
					msgText.Text = GooglePlayServicesUtil.GetErrorString (resultCode);
				else
				{
					msgText.Text = "Sorry, this device is not supported";
					Finish ();
				}
				return false;
			}
			else
			{
				msgText.Text = "Google Play Services is available.";
				return true;
			}
		}
	}
}


