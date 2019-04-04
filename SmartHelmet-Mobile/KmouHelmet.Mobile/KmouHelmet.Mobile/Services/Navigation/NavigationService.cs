using System.Threading.Tasks;
using Xamarin.Forms;

namespace KmouHelmet.Mobile.Services.Navigation
{
    public static class NavigationService
    {
        static bool _isNavigating;

        /// <summary>
        /// PUsh a page async
        /// </summary>
        /// <returns>awaitable task.</returns>
        /// <param name="navigation">Navigation.</param>
        /// <param name="page">Page.</param>
        /// <param name="animate">If set to <c>true</c> animate.</param>
        public static async Task PushAsync(INavigation navigation, Page page, bool animate = false)
        {
            if (_isNavigating)
            {
                return;
            }

            _isNavigating = true;
            await navigation.PushAsync(page, animate);
            _isNavigating = false;
        }

        /// <summary>
        /// Push a page modal async
        /// </summary>
        /// <returns>awaitable task.</returns>
        /// <param name="navigation">Navigation.</param>
        /// <param name="page">Page.</param>
        /// <param name="animate">If set to <c>true</c> animate.</param>
        public static async Task PushModalAsync(INavigation navigation, Page page, bool animate = false)
        {
            if (_isNavigating)
            {
                return;
            }

            _isNavigating = true;
            await navigation.PushModalAsync(page, animate);
            _isNavigating = false;
        }
    }
}
