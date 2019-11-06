using Xamarin.Forms;
[assembly:Dependency(typeof(BookSwapRis.iOS.Implementation.ViewLocationFetcher))]
namespace BookSwapRis.iOS.Implementation
{
    using System.Drawing;
    using DependencyServiceInterfaces; //Folder Name on Shared Project
    using Xamarin.Essentials;

    public class ViewLocationFetcher : IViewLocationFetcher
    {
        public PointF GetCoordinates(VisualElement view)
        {
            var renderer = Xamarin.Forms.Platform.iOS.Platform.GetRenderer(view);
            if (renderer == null)
                return new PointF();

            var nativeView = renderer.NativeView;
            var react = nativeView.Superview.ConvertPointFromView(nativeView.Frame.Location, null);
            return react.ToSystemPointF();
        }
    }
}