using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;          // for Intent

using Android.Provider;
using Android.Graphics;

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
        
        // For camera intent
        Button _showCamera;
        ImageView _imageView;
        Java.IO.File _file;
        Java.IO.File _dir;

        Button _locationBasic;
        Button _locationAdvanced;

        private Button _showStorage;

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
            _showCamera =   FindViewById<Button>(Resource.Id.button4);
            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            _locationBasic = FindViewById<Button>(Resource.Id.button5);
            _locationAdvanced = FindViewById<Button>(Resource.Id.button6);
            _showStorage = FindViewById<Button>(Resource.Id.button7);

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

            _showCamera.Click += (object sender, EventArgs e) =>
            {
                // Create public photos directory on external storage
                _dir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "AndroidCameraVSDemo");
                if (!_dir.Exists())
                {
                    _dir.Mkdirs();
                }

                // Setup the camera intent
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                // Setup the file name for the photo
                _file = new Java.IO.File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
                // Pass the filename of the photo to the camera intent
                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
                // Start the camera!
                StartActivityForResult(intent, 0);

            };

            // launch event to get basic location once
            _locationBasic.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(locationBasic));
                StartActivity(intent);
            };

            // launch event to get advanced location with updates
            _locationAdvanced.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(locationAdvanced));
                StartActivity(intent);
            };

            _showStorage.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(storage));
                StartActivity(intent);
            };

        }
        // Event fires after photo is taken
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Canceled) return;

            if (_file != null)
            {
                // Create a bitmap from the photo data using its file path
                Bitmap bitmap = BitmapFactory.DecodeFile(_file.Path);

                // scale the photo to 20% of its size for displaying
                int scaleWidth = (int)(bitmap.Width * 0.2);
                int scaleHeight = (int)(bitmap.Height * 0.2);

                // rescale the photo and bind it to the imageview widget
                var bitmapScalled = Bitmap.CreateScaledBitmap(bitmap, scaleWidth, scaleHeight, true);
                bitmap.Recycle();
                _imageView.SetImageBitmap(bitmapScalled);

                // garbage collection to remove any large images from memory
                GC.Collect();
            }
        }
    }
}

