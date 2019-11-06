
using Xamarin.Forms;

[assembly:Dependency(typeof(BookSwapRis.Droid.Implementation.ViewLocationFetcher))]
namespace BookSwapRis.Droid.Implementation
{
    //no olvidar el assembly de arriba
    using DependencyServiceInterfaces; //Folder Name on Shared Project
    using System.Drawing;
    using Xamarin.Essentials;

    public class ViewLocationFetcher : IViewLocationFetcher
    {
        public PointF GetCoordinates(VisualElement view)
        {
            var renderer = Xamarin.Forms.Platform.Android.Platform.GetRenderer(view);
            if (renderer == null)
                return new PointF();

            var nativeView = renderer.View;
            var location = new int[2];
            var density = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
            nativeView.GetLocationOnScreen(location);

            return new PointF(location[0] / (float)density, location[1] / (float)density);


        }
    }
}