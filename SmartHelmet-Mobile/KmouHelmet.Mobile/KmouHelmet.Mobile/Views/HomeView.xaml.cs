using System.IO;
using System.Reflection;
using KmouHelmet.Mobile.ViewModels;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace KmouHelmet.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : BaseContentPage<HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();

            Assembly assembly = typeof(HomeView).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("KmouHelmet.Mobile.Styles.map_style.json");
            string json = "";
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            MainMap.MapStyle = MapStyle.FromJson(json);
        }
    }
}
