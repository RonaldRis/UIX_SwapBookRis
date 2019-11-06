using BookSwapRis.DependencyServiceInterfaces;
using BookSwapRis.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookSwapRis.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookViewCell : ViewCell
    {

        //Variable
        private SKPaint _backgroudColor;
        private SKPaint _DarkColor;
        private SKPaint _ExtraDarkColor;

        float scrollValue;

        #region Solucion Donde se mueve relativamente usando Dependency Service del IViewLocationFetcher
        
        
        IViewLocationFetcher viewlocationfetcher;
        public BookViewCell()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<MessagingCenterAttributes, float>(new MessagingCenterAttributes(),
                                                                        MessagingCenterAttributes.ScrollMessage,
                                                                        (sender, info_y) =>
                                                                        {
                                                                            scrollValue = info_y;

                                                                            XAML_CanvasView.InvalidateSurface();
                                                                        }

                                                                );
            

            viewlocationfetcher = DependencyService.Get<IViewLocationFetcher>();

        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (this.BindingContext != null)
            {
                var color = ((Book)BindingContext).Colors.Accent;
                _backgroudColor = new SKPaint() { Color = color.ToSKColor() };
                _DarkColor = new SKPaint() { Color = color.AddLuminosity(-0.1).ToSKColor() };
                _ExtraDarkColor = new SKPaint() { Color = color.AddLuminosity(-0.15).ToSKColor() };
            }
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();
            //Implemento el Dependency Service y uso el metodo segun la plataforma en ejecu
            var thisCellPosition = viewlocationfetcher.GetCoordinates(this.View);

            canvas.DrawRect(e.Info.Rect, _backgroudColor);
            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                //path.LineTo((e.Info.Width * 0.9f) -thisCellPosition.Y, 0); 
                path.LineTo(e.Info.Width - thisCellPosition.Y, 0); //Both Looks Well
                path.LineTo(0, e.Info.Height);
                path.Close();

                canvas.DrawPath(path, _DarkColor);
            }

            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                path.LineTo(0.35f * scrollValue, 0);
                //path.LineTo(e.Info.Width * 0.5f  - thisCellPosition.Y, 0);
                path.LineTo(e.Info.Width - (thisCellPosition.Y * 2f), 0); //Both Work Well

                path.LineTo(0, e.Info.Height * 0.5f);
                path.Close();
                canvas.DrawPath(path, _ExtraDarkColor);
            }


        }
        #endregion



        #region Solucion Donde todos se mueven al mismo paso
        /**
        public BookViewCell()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<MessagingCenterAttributes, float>(new MessagingCenterAttributes(),
                                                                        MessagingCenterAttributes.ScrollMessage,
                                                                        (sender, info_y) =>
                                                                        {
                                                                            scrollValue = info_y;

                                                                            XAML_CanvasView.InvalidateSurface();
                                                                        }

                                                                );
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (this.BindingContext != null)
            {
                var color = ((Book)BindingContext).Colors.Accent;
                _backgroudColor = new SKPaint() { Color = color.ToSKColor() };
                _DarkColor = new SKPaint() { Color = color.AddLuminosity(-0.1).ToSKColor() };
                _ExtraDarkColor = new SKPaint() { Color = color.AddLuminosity(-0.15).ToSKColor() };
            }
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();

            canvas.DrawRect(e.Info.Rect, _backgroudColor);
            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                //path.LineTo(e.Info.Width * 0.7f, 0);
                path.LineTo(0.7f * scrollValue, 0);
                path.LineTo(0, e.Info.Height);
                path.Close();

                canvas.DrawPath(path, _DarkColor);
            }

            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                path.LineTo(0.35f * scrollValue, 0);
                //path.LineTo(e.Info.Width * 0.35f,0);
                path.LineTo(0, e.Info.Height * 0.5f);
                path.Close();
                canvas.DrawPath(path, _ExtraDarkColor);
            }


        }

    **/

        #endregion
    }
}