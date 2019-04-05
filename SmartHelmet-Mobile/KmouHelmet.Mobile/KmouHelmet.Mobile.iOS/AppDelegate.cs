using Foundation;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.iOS;

namespace KmouHelmet.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            InitRenderersAndServices();

            Forms.Init();

            RegisterPlatformServices();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void InitRenderersAndServices()
        {
            FormsGoogleMaps.Init(AppSettings.GoogleMapsApiKey);
            Material.Init();
        }

        private void RegisterPlatformServices()
        {
        }
    }
}
