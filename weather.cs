using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace IoT_Android
{
    [Activity(Label = "weather")]
    public class weather : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            // Set our view from the "weatherLayout" layout resource
            SetContentView(Resource.Layout.weatherLayout);

            // Use a hardcoded latitude and longitude and create a query for the weather service
            // The username paramter is used to authenticate requests to the service and for rate limiting purposes
            string url = "http://api.geonames.org/findNearByWeatherJSON?lat=53.2&lng=-0.54&username=derekfoster";

            // Fetch the weather information asynchronously using 'await'
            JsonValue json = await FetchWeatherAsync(url);

            // parse the results, then update the screen:
            ParseAndDisplay(json);


        }

        private async Task<JsonValue> FetchWeatherAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }

        // Parse the weather data, then write temperature, humidity, 
        // conditions, and location to the screen.
        private void ParseAndDisplay(JsonValue json)
        {
            // Get the weather reporting fields from the layout resource:
            TextView location = FindViewById<TextView>(Resource.Id.locationText);
            TextView temperature = FindViewById<TextView>(Resource.Id.tempText);
            TextView humidity = FindViewById<TextView>(Resource.Id.humidText);
            TextView conditions = FindViewById<TextView>(Resource.Id.condText);

            // Extract the array of name/value results for the field name "weatherObservation". 
            JsonValue weatherResults = json["weatherObservation"];

            // Extract the "stationName" (location string) and write it to the location TextBox:
            location.Text = weatherResults["stationName"];

            // The temperature is expressed in Celsius:
            double temp = weatherResults["temperature"];
            // Convert it to Fahrenheit:
            temp = ((9.0 / 5.0) * temp) + 32;
            // Write the temperature (one decimal place) to the temperature TextBox:
            temperature.Text = String.Format("{0:F1}", temp) + "° F";

            // Get the percent humidity and write it to the humidity TextBox:
            double humidPercent = weatherResults["humidity"];
            humidity.Text = humidPercent.ToString() + "%";

            // Get the "clouds" and "weatherConditions" strings and 
            // combine them. Ignore strings that are reported as "n/a":
            string cloudy = weatherResults["clouds"];
            if (cloudy.Equals("n/a"))
                cloudy = "";
            string cond = weatherResults["weatherCondition"];
            if (cond.Equals("n/a"))
                cond = "";
            ;
            // Write the result to the conditions TextBox:
            conditions.Text = cloudy + " " + cond;
        }

    }
}