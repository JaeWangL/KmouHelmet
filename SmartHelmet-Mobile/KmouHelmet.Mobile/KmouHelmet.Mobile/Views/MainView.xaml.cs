using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KmouHelmet.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : MasterDetailPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Detail = new CustomNavigationView(new HomeView());
        }
    }
}
