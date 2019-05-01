using KmouHelmet.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace KmouHelmet.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : BaseContentPage<MenuViewModel>
    {
        public MenuView()
        {
            InitializeComponent();

            BindingContext = new MenuViewModel();
        }
    }
}
