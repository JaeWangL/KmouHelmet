using System.Drawing;
using KmouHelmet.Mobile.Dtos;
using KmouHelmet.Mobile.ViewModels;
using Xamarin.Essentials;
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

            DeviceList.ItemTapped += (sender, e) => DeviceList.SelectedItem = null;
            DeviceList.ItemSelected += async (sender, e) =>
            {
                if (!(DeviceList.SelectedItem is DeviceDto device))
                {
                    return;
                }

                await Browser.OpenAsync(device.StreamingUrl, new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Color.AliceBlue,
                    PreferredControlColor = Color.Violet
                });

                DeviceList.SelectedItem = null;
            };
        }
    }
}
