using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Json;
using Newtonsoft.Json;

namespace CallWebSericeDemo
{
	[Activity (Label = "CallWebSericeDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			EditText latitude = FindViewById<EditText> (Resource.Id.latText);
			EditText longitude = FindViewById<EditText> (Resource.Id.longText);
			Button button = FindViewById<Button> (Resource.Id.getWeatherButton);
			
			button.Click += async delegate {
				// Get the latitude and longitude entered by the user and create a query.
				string url = "http://api.geonames.org/findNearByWeatherJSON?lat="+latitude.Text+"&lng="+longitude.Text+"&username=demo";

                // Fetch the weather information asynchronously, 
                // parse the results, then update the screen:
				Weather responData = FetchWeatherAsync(url);

                ParseAndDisplay(responData);
            };
		}

        //private async Task<JsonValue> FetchWeatherAsync(string url)
        //{
        //    // Create an HTTP web request using the URL:
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
        //    request.ContentType = "application/json";
        //    request.Method = "GET";

        //    // Send the request to the server and wait for the response:
        //    using (WebResponse response = await request.GetResponseAsync())
        //    {
        //        // Get a stream representation of the HTTP web response:
        //        using (Stream stream = response.GetResponseStream())
        //        {
        //            // Use this stream to build a JSON document object:
        //            JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
        //            Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

        //            // Return the JSON document:
        //            return jsonDoc;
        //        }
        //    }
        //}

        private Weather FetchWeatherAsync(string url)
        {
            Weather w = new Weather();
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response =  request.GetResponse())
            {
                // Get a stream representation of the HTTP web response:
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    var streamContent = stream.ReadToEnd();
					w = JsonConvert.DeserializeObject<Weather>(streamContent);
                    
                    // Return the JSON document:
					return w;
                }
            }
        }

        private void ParseAndDisplay(Weather dataObj)
        {
            // Get the weather reporting fields from the layout resource:
            TextView location = FindViewById<TextView>(Resource.Id.locationText);
            TextView temperature = FindViewById<TextView>(Resource.Id.tempText);
            TextView humidity = FindViewById<TextView>(Resource.Id.humidText);
            TextView conditions = FindViewById<TextView>(Resource.Id.condText);

            // Extract the array of name/value results for the field name "weatherObservation". 
            //JsonValue weatherResults = json["weatherObservation"];

            // Extract the "stationName" (location string) and write it to the location TextBox:
            location.Text = dataObj.weatherObservation.stationName;

            // The temperature is expressed in Celsius:
            double temp = dataObj.weatherObservation.temperature;
            // Convert it to Fahrenheit:
            temp = ((9.0 / 5.0) * temp) + 32;
            // Write the temperature (one decimal place) to the temperature TextBox:
            temperature.Text = String.Format("{0:F1}", temp) + "° F";

            // Get the percent humidity and write it to the humidity TextBox:
            double humidPercent = dataObj.weatherObservation.humidity;
            humidity.Text = humidPercent.ToString() + "%";

            // Get the "clouds" and "weatherConditions" strings and 
            // combine them. Ignore strings that are reported as "n/a":
            string cloudy = dataObj.weatherObservation.clouds;
            if (cloudy.Equals("n/a"))
                cloudy = "";
            string cond = dataObj.weatherObservation.weatherCondition;
            if (cond.Equals("n/a"))
                cond = "";

            // Write the result to the conditions TextBox:
            conditions.Text = cloudy + " " + cond;
        }
    }
}


