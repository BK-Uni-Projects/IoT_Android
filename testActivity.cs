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

            // get parameter value as passed from MainActivity.cs
            var nameEntered = Intent.Extras.GetString("text_entered");

            // programmatically create a new TextView widget called tv
            TextView tv = new TextView(this);

            //set the font size of the widget to 40
            tv.SetTextSize(Android.Util.ComplexUnitType.Dip, 40f);

            // bind the string value of the pass paramter to the text widget
            tv.Text = nameEntered;

            // set the current layout to the textview widget
            SetContentView(tv);


            // Create your application here
        }
    }
}