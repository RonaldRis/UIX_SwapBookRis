using BookSwapRis.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace BookSwapRis
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        //Variable
        private SKPaint _backgroudColor;
        private SKPaint _DarkColor;
        private SKPaint _ExtraDarkColor;


        public MainPage()
        {
            InitializeComponent();

            /// XFUtils
            var eff = new XFUtils.Effects.ScrollReporterEffect();
            eff.ScrollChanged += Eff_ScrollChanged;
            XAML_listview.Effects.Add(eff);
        }

        private void Eff_ScrollChanged(object sender, XFUtils.Effects.ScrollEventArgs args)
        {
            MessagingCenter.Send<MessagingCenterAttributes,float>(new MessagingCenterAttributes(),
                                                                  MessagingCenterAttributes.ScrollMessage,
                                                                  (float)args.Y);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _backgroudColor = new SKPaint() { Color = Color.FromHex("FFF571").ToSKColor() };
            _DarkColor = new SKPaint() { Color = Color.FromHex("FFF571").AddLuminosity(-0.1).ToSKColor() };
            _ExtraDarkColor = new SKPaint() { Color = Color.FromHex("FFF571").AddLuminosity(-0.15).ToSKColor() };
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {

            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();

            canvas.DrawRect(e.Info.Rect, _backgroudColor);
            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                path.LineTo(e.Info.Width * 0.8f, 0);
                path.LineTo(e.Info.Width * 0.5f, e.Info.Height);
                path.LineTo(0, e.Info.Height);
                path.Close();

                canvas.DrawPath(path, _DarkColor);
            }

            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                path.LineTo(e.Info.Width * 0.5f, 0);
                path.LineTo(0, e.Info.Height * 0.5f);
                path.Close();
                canvas.DrawPath(path, _ExtraDarkColor);
            }


        }






        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Debug.Print(sender.ToString());
            Debug.Print(e.Direction+ "--"+e.Parameter +"---"+ e.ToString());
        }

        private void SwipeGestureRecognizer_Swiped_1(object sender, SwipedEventArgs e)
        {
            Debug.Print(sender.ToString());
            Debug.Print(e.Direction + "--" + e.Parameter + "---" + e.ToString());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Debug.Print(sender.ToString());
            Debug.Print( e.ToString());
        }

    }
}
