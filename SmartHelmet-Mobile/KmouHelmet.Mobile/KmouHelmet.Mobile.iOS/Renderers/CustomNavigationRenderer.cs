using System.ComponentModel;
using System.Threading.Tasks;
using KmouHelmet.Mobile.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationRenderer))]
namespace KmouHelmet.Mobile.iOS.Renderers
{
    public class CustomNavigationRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Element.PropertyChanged += HandlePropertyChanged;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var navPage = (NavigationPage)Element;
                navPage.PropertyChanged -= HandlePropertyChanged;
            }

            base.Dispose(disposing);
        }

        void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NavigationPage.BarTextColorProperty.PropertyName
                || e.PropertyName == Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.StatusBarTextColorModeProperty.PropertyName)
            {
                UpdateStatusBarStyle();
            }
        }

        async void UpdateStatusBarStyle()
        {
            // we want to change defaults XF status bar style calcs
            await Task.Delay(1);
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
        }
    }
}
