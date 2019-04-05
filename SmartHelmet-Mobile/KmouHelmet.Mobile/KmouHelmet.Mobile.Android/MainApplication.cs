using Android.App;
using Android.Runtime;
using Plugin.CurrentActivity;
using System;

namespace KmouHelmet.Mobile.Droid
{
    [Application(Debuggable = false)]
    [MetaData("com.google.android.maps.v2.API_KEY",
        Value = AppSettings.GoogleMapsApiKey)]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
            : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            CrossCurrentActivity.Current.Init(this);
        }
    }
}
