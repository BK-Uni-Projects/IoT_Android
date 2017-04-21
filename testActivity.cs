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

namespace IoT_Android
{
    [Activity(Label = "testActivity")]
    public class testActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "test.axml" layout resource
            SetContentView(Resource.Layout.test);
            // get parameter value as passed from MainActivity.cs
            var nameEntered = Intent.Extras.GetString("text_entered");
            // create instance of the textView in test.axml
            TextView showName;
            // find the textView
            showName = FindViewById<TextView>(Resource.Id.textView1);
            // bind the value of name_entered to the textView
            showName.Text = nameEntered;

        }
    }
}