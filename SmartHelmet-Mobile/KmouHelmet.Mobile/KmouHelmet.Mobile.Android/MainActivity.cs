using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace KmouHelmet.Mobile.Droid
{
    [Activity(
        Label = "@string/appName",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            InitRenderersAndServices(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            PermissionsImplementation.Current.OnRequestPermissionsResult(
                requestCode,
                permissions,
                grantResults);
        }

        private void InitRenderersAndServices(Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            FormsGoogleMaps.Init(this, savedInstanceState);
        }
    }
}
