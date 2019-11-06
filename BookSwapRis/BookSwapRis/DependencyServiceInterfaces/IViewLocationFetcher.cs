namespace BookSwapRis.DependencyServiceInterfaces
{
    public interface IViewLocationFetcher
    {
        System.Drawing.PointF GetCoordinates(Xamarin.Forms.VisualElement view);
    }
}
