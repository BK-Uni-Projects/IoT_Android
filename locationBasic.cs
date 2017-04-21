using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Locations;
using Android.Util;

namespace IoT_Android
{
    [Activity(Label = "locationBasic")]
    public class locationBasic : Activity
    {
        // create new instance of the Android Location Manager
        LocationManager locMgr;
        // select the GPS provider for location data
        string Provider = LocationManager.GpsProvider;
        // variables to store latitude and longitude of last known location
        double lat;
        double longi;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            locMgr = GetSystemService(Context.LocationService) as LocationManager;

            // check if a last known location exists
            

            // check if GPS location provider is available
            if (locMgr != null && locMgr.IsProviderEnabled(Provider))
            {
                // get the last known location from the location manager
                Location lastKnownLocation = locMgr.GetLastKnownLocation(Provider);

                // bind the lat/long coordinates of last known location t0 double variables
                // if no last location is available set lat/long to zero
                if (lastKnownLocation == null)
                {
                    lat = 0;  // lat of Lincoln is 53.228029;
                    longi = 0; // longi of Lincoln is -0.546055;
                }
                else
                { 
                    lat = lastKnownLocation.Latitude;
                    longi = lastKnownLocation.Longitude;
                }
            }
            else
            {
                Log.Info("Location error, ", Provider + " is not available. Does the device have location services enabled?");
            }

            // programmatically create a textview widget to display
            TextView showLocation = new TextView(this);

            // bind the value of name_entered to the textView
            showLocation.Text = ("Loc:\t" + lat + "\t" + longi);

            // Set the UI view to the textView widger showLocation
            SetContentView(showLocation);

            //This gets the full path for the "files" directory of your app, where you have permission to read/write.
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            //This creates the full file path to your "location.txt" file.
            var filePath = System.IO.Path.Combine(documentsPath, "location.txt");

            // Create a streamwriter that will create the file and append data to it
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                // Write location cooordinates to a line in text file
                writer.WriteLine(lat + "," + longi + "\n");
                ;
            }


        }
    }
}