using Android.Content;
using Android.Support.V7.Widget;
using KmouHelmet.Mobile.Droid.Renderers;
using KmouHelmet.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomNavigationView), typeof(CustomNavigationViewRenderer))]
namespace KmouHelmet.Mobile.Droid.Renderers
{
    public class CustomNavigationViewRenderer : NavigationPageRenderer
    {
        IPageController PageController => Element as IPageController;
        CustomNavigationView CustomNavigationPage => Element as CustomNavigationView;

        public CustomNavigationViewRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            CustomNavigationPage.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            CustomNavigationPage.IgnoreLayoutChange = false;

            var containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (var i = 0; i < ChildCount; i++)
            {
                Android.Views.View child = GetChildAt(i);
                if (child is Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }
    }
}
