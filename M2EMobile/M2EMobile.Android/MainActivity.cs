using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;

namespace M2EMobile.Droid
{
    [Activity(Label = "M2EMobile", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            SetPage(App.GetMainPage());
        }

        public async override void OnBackPressed()
        {
            bool? result = await App.CallHardwareBackPressed();
            if (result == true)
            {
                base.OnBackPressed();
            }
            else if (result == null)
            {
                Finish();
            }
        }
    }
}

