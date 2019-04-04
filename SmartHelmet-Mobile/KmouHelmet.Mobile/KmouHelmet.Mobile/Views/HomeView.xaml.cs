using KmouHelmet.Mobile.ViewModels;
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
        }
    }
}
