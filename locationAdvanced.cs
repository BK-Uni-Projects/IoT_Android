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

using Android.Locations;
using Android.Util;


namespace IoT_Android
{
    [Activity(Label = "locationAdvanced")]
    public class locationAdvanced : Activity, ILocationListener
    {
        // create new instance of the Android Location Manager
        LocationManager locMgr;
        // select the GPS provider for location data
        string Provider = LocationManager.GpsProvider;
        // variables to store latitude and longitude of current/last location
        double lat;
        double longi;
        TextView showLocation;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            // programmatically create a textview widget to display coordinates
            showLocation = new TextView(this);

            // Set the UI view to the textView widger showLocation
            showLocation.Text = "No location at present";
            SetContentView(showLocation);
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            showLocation.Text = ("Status: Changed, maybe FUBAR, #oops");
        }

        public void OnLocationChanged(Location location)
        {
            // check if a last known location exists
            // if no last location is available set lat/long to zero
            if (location == null)
            {
                lat = 0;
                longi = 0;
            }
            else
            {  
                lat = location.Latitude;
                longi = location.Longitude;
            }
           
            showLocation.Text = ("Location:\n" + lat + "\n" + longi);
            
        }

        public void OnProviderDisabled(string provider)
        {
            showLocation.Text = ("Provider:" + provider + " Disabled");
        }

        public void OnProviderEnabled(string provider)
        {
            showLocation.Text = ("Provider:" + provider + " Enabled");
        }

        protected override void OnResume()
        {
            base.OnResume();
            locMgr = GetSystemService(Context.LocationService) as LocationManager;
            string Provider = LocationManager.GpsProvider;

            // get the last known location from the location manager first
            Location lastKnownLocation = locMgr.GetLastKnownLocation(Provider);
            lat = lastKnownLocation.Latitude;
            longi = lastKnownLocation.Longitude;
            showLocation.Text = ("Location:\n" + lat + "\n" + longi);

            if (locMgr.IsProviderEnabled(Provider))
            {
                locMgr.RequestLocationUpdates(Provider, 2000, 1, this);
            }
            else
            {
                Log.Info("update error: ", Provider + " is not available. Does the device have location services enabled?");
            }
        }
    }
}