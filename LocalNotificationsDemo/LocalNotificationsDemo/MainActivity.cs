using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;

namespace LocalNotificationsDemo
{
	[Activity (Label = "LocalNotificationsDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
		private static readonly int ButtonClickNotificationId = 1000;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += ButtonOnClick;
        }

		private void ButtonOnClick (object sender, EventArgs eventArgs)
		{
			// Pass the current button press count value to the next activity:
			Bundle valuesForActivity = new Bundle();
			valuesForActivity.PutInt ("count", count);

			// When the user clicks the notification, SecondActivity will start up.
			Intent resultIntent = new Intent(this, typeof (SecondActivity));

			// Pass some values to SecondActivity:
			resultIntent.PutExtras (valuesForActivity);

            // Construct a back stack for cross-task navigation:
            Android.App.TaskStackBuilder stackBuilder = Android.App.TaskStackBuilder.Create (this);
			stackBuilder.AddParentStack (Java.Lang.Class.FromType(typeof(SecondActivity)));
			stackBuilder.AddNextIntent (resultIntent);

			// Create the PendingIntent with the back stack:            
			PendingIntent resultPendingIntent = 
				stackBuilder.GetPendingIntent (0, PendingIntentFlags.UpdateCurrent);


            Notification.InboxStyle inboxStyle = new Notification.InboxStyle();
            // Build the notification:
            NotificationCompat.Builder builder = new NotificationCompat.Builder (this)
				.SetAutoCancel (true)                    // Dismiss from the notif. area when clicked
				.SetContentIntent (resultPendingIntent)  // Start 2nd activity when the intent is clicked.
				.SetContentTitle ("Button Clicked")      // Set its title
				.SetNumber (count)                       // Display the count in the Content Info
				.SetSmallIcon(Resource.Drawable.Icon)  // Display this icon
				.SetContentText (String.Format(
					"The button has been clicked {0} times.", count)); // The message to display.
            builder.SetDefaults((int)NotificationDefaults.Sound);

            // Finally, publish the notification:
            NotificationManager notificationManager = 
				(NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Notify(ButtonClickNotificationId, builder.Build());

			// Increment the button press count:
			count++;
		}
	}
}


