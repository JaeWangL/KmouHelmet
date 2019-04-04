using KmouHelmet.Mobile.Services.Location;
using KmouHelmet.Mobile.Services.Request;
using KmouHelmet.Mobile.Views;
using Xamarin.Forms;
using XF.Material.Forms;

namespace KmouHelmet.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Material.Init(this);

            RegisterServicesAndProviders();

            MainPage = new MainView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void RegisterServicesAndProviders()
        {
            DependencyService.Register<ILocationService, LocationService>();
            DependencyService.Register<IRequestService, RequestService>();
        }
    }
}
