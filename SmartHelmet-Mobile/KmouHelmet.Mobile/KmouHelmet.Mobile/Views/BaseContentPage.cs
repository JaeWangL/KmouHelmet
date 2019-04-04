using KmouHelmet.Mobile.ViewModels;
using Xamarin.Forms;

namespace KmouHelmet.Mobile.Views
{
    public abstract class BaseContentPage<T> : ContentPage
        where T : BaseViewModel
    {
        bool _isAlreadyInitialized;
        bool _isAlreadyUninitialized;

        protected virtual T ViewModel => BindingContext as T;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!_isAlreadyInitialized)
            {
                ViewModel.InitializeAsync();
                _isAlreadyInitialized = true;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (!_isAlreadyUninitialized)
            {
                ViewModel.UninitializeAsync();
                _isAlreadyUninitialized = true;
            }
        }
    }
}
