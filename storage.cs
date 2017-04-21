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

using Android.Preferences;

namespace IoT_Android
{
    [Activity(Label = "storage")]
    public class storage : Activity
    {
        // Get our UI controls from the loaded layout:
        Button saveButton;
        Button viewButton;
        EditText firstname_input;
        EditText surname_input;
        TextView show_firstname;
        TextView show_surname;

        // Add reference to shared preferences
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.storageui);

            // UI widgets for entering text and saving
            firstname_input = FindViewById<EditText>(Resource.Id.editText1);
            surname_input = FindViewById<EditText>(Resource.Id.editText2);
            saveButton = FindViewById<Button>(Resource.Id.button1);
            // UI widgets for viewing saved text
            show_firstname = FindViewById<TextView>(Resource.Id.textView1);
            show_surname = FindViewById<TextView>(Resource.Id.textView2);
            viewButton = FindViewById<Button>(Resource.Id.button2);
            
            saveButton.Click += delegate {
                //Create instance of shared preferences
                prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                //Start editing mode
                editor = prefs.Edit();
                //Place values of input text into shared preference keys (firstname, surname)
                editor.PutString("firstname", firstname_input.Text);
                editor.PutString("surname", surname_input.Text);
                // Commit changes (don't forget this!)
                editor.Apply();
            };

            viewButton.Click += delegate {
                //Create instance of shared preferences
                prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                //Get key values from shared preferences for firstname and surname keys
                string firstname = prefs.GetString("firstname", "no value");
                string surname = prefs.GetString("surname", "no value");
                //Bind key values to textView widgets
                show_firstname.Text = firstname;
                show_surname.Text = surname;
            };



        }
    }
}