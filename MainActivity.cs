using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;          // for Intent

using Android.Provider;
using Android.Graphics;

/**
 * Pre-requisite installs - nuget
 *          Install-Package Newtonsoft.Json
 *          Install-Package OxyPlot.Xamarin.Android -Version 1.0.0
 * 
 **/

namespace IoT_Android
{
    [Activity(Label = "IoT_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        // declaration of UI controls
        private Button _showStorage;
        private Button _sensors;

        //Java.IO.File _file;
        //Java.IO.File _dir;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

         
            /* Storage ASctivities */
            _showStorage = FindViewById<Button>(Resource.Id.button7);
            _showStorage.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(storage));
                StartActivity(intent);
            };

            /*Sensors Activities*/
            _sensors = FindViewById<Button>(Resource.Id.button10);
            _sensors.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(sensors));
                StartActivity(intent);
            };

       
        }
    }
}

