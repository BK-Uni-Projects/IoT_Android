using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Mime;
using Android.App;
using Android.Content;
using Android.Hardware.Display;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;


namespace IoT_Android
{
    [Activity(Label = "sensors")]
    public class sensors : Activity
    {
        private Button _graphs;

        public class jsonSensor
        {
            public string sensor_type { get; set; }
            public string value { get; set; }
        }

     
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.sensors);

            /*Graphs Activities*/
            _graphs = FindViewById<Button>(Resource.Id.button1);
            _graphs.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(graphs));
                StartActivity(intent);
            };


            string sensorURL = "http://bksiotworkshop.azurewebsites.net/index.php/sensors/getsensordata?sensorid=5";
            

            JsonValue json = await GetSensorData(sensorURL);

            TextView jsondump1 = FindViewById<TextView>(Resource.Id.textView1);
            TextView jsondump2 = FindViewById<TextView>(Resource.Id.textView2);

            jsondump1.Text = "text"; //json.ToString();           // Output Pure json as returned by GET
            jsondump2.Text = ParseAndDisplay(json);     // Output formatted json 

        }


        private async Task<JsonValue> GetSensorData(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonValue.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    return jsonDoc;
                }
            }
        }


        private string ParseAndDisplay(JsonValue json)
        {
            string jsonlist = "";
            var items = JsonConvert.DeserializeObject<List<jsonSensor>>(json.ToString());
            TextView outputjson =    FindViewById<TextView>(Resource.Id.textView1);
            
            // loop through entire json document and list each item
            for (int x = 0; x < items.Count; x++)
            {
                Console.Out.WriteLine("Response {0}: {1};", x, items[x].sensor_type);
                jsonlist += $"S{x}: type={items[x].sensor_type}, val={items[x].value} \n";
            }

            return jsonlist;
        }

        

    }

    
}