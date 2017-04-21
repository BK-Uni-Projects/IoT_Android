using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;          // for Intent

namespace IoT_Android
{
    [Activity(Label = "IoT_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        // declaration of UI controls
        private Button _helloButton;
        private EditText _nameInput;
        private TextView _showName;
        private Button _showBrowser;
        private Button _showMaps;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Assign UI controls
            _helloButton =  FindViewById<Button>(Resource.Id.button1);
            _nameInput =    FindViewById<EditText>(Resource.Id.editText1);
            _showName =     FindViewById<TextView>(Resource.Id.textView1);
            _showBrowser =  FindViewById<Button>(Resource.Id.button2);
            _showMaps =     FindViewById<Button>(Resource.Id.button3);

            // Functions for UI controls
            _helloButton.Click += (object sender, EventArgs e) =>
            {
                _showName.Text = _nameInput.Text.ToString();

                // create new intent instance for testActivity
                var intent = new Intent(this, typeof(testActivity));

                // load the intent with a parameter containing the string value in showName
                intent.PutExtra("text_entered", _showName.Text);

                // start the activity and pass the parameter to testActivity
                StartActivity(intent);
            };

            // button event to launch browser
            _showBrowser.Click += delegate {
                // parse web url to a Uri
                var uri = Android.Net.Uri.Parse("http://www.android.com");

                // setup intent to launch browser with the uri web address
                var intent = new Intent(Intent.ActionView, uri);

                // launch browser intent
                StartActivity(intent);
            };

            // launch event for Google maps
            _showMaps.Click += delegate {
                // create Uri with GPS coordinates of Lincoln
                var geoUri = Android.Net.Uri.Parse("geo:53.22683 -0.53792");
                // pass coordinates to intent app that is capable of display geocoordinates
                var mapIntent = new Intent(Intent.ActionView, geoUri);
                // launch Google maps or show list of map apps to choose from
                StartActivity(mapIntent);
            };
        }
    }
}

