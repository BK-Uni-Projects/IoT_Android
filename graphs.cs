using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

using OxyPlot.Xamarin.Android;

namespace IoT_Android
{
    [Activity(Label = "graphs")]
    public class graphs : Activity
    {

        public class jsonSensor
        {
            public string sensor_type { get; set; }
            public string value { get; set; }
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.graphs);

            

            var view = FindViewById<PlotView>(Resource.Id.plot_view);

            view.Model = await CreatePlotModel();

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


        private LineSeries CreateSeries(JsonValue json)
        {

            var series = new LineSeries
            {
                MarkerType = MarkerType.Cross,
                MarkerSize = 2,
                MarkerStroke = OxyColors.Aqua
            };

            var items = JsonConvert.DeserializeObject<List<sensors.jsonSensor>>(json.ToString());
            
            // loop through json document
            for (int x = 0; x < items.Count; x++)
            {
                Console.Out.WriteLine("Response {0}: {1};", x, items[x].value);
                series.Points.Add(new DataPoint(Convert.ToDouble(x), Convert.ToDouble(items[x].value)));
            }

            return series;
        }



        private async Task<PlotModel> CreatePlotModel()
        {

            string sensorURL = "http://bksiotworkshop.azurewebsites.net/index.php/sensors/getsensordata?sensorid=6";
            JsonValue json = await GetSensorData(sensorURL);

            var plotModel = new PlotModel { Title = "Temperature Sensor 01" };

            plotModel.Background.GetActualColor(OxyColors.LightYellow);

            plotModel.Background.ChangeIntensity(5);

            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = 65, Minimum = 0 });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 60, Minimum = 25 });

            var series1 = CreateSeries(json);     // Create plot info from json data

            plotModel.Series.Add(series1);

            return plotModel;
        }

    }
}