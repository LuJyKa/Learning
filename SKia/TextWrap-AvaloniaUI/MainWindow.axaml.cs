using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;
using System.Diagnostics;
using System;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace TextWrap_AvaloniaUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SkiaCanvas.DrawOP = _DrawOP_TextWrap;
            ///this._DrawOP_TextWrap.Bounds = new Rect(10, this.Slider.Bounds.Height + 10, this.Bounds.Height - 20, this.Bounds.Height - this.Slider.Bounds.Height - 20);
            this._DrawOP_TextWrap.Bounds = new Rect(0,0,1,1);
        }


        private void Slot_RangeBase_OnValueChanged(object? pSender, RangeBaseValueChangedEventArgs pE)
        {
            var tSlider = (Slider)pSender!;
            var tWidth = this.Bounds.Width * (tSlider.Value / tSlider.Maximum);
            this._DrawOP_TextWrap.Bounds = new Rect(10, tSlider.Bounds.Height + 10, tWidth - 20, this.Bounds.Height - tSlider.Bounds.Height - 20);
        }

        protected override void OnSizeChanged(SizeChangedEventArgs e)
        {
            base.OnSizeChanged(e);
            ///
        }

        private DrawOP_TextWrap _DrawOP_TextWrap = new();
        public override void Render(DrawingContext context)
        {
            base.Render(context);
        }


        //public override void Render(DrawingContext context)
        //{
        //    base.Render(context);
        //    if (this.IsLoaded)
        //    {
        //        _DrawOP_TextWrap.Bounds = new Rect(10, this.Slider.Bounds.Height + 10, this.Bounds.Height - 20, this.Bounds.Height - this.Slider.Bounds.Height - 20);
        //        context.Custom(_DrawOP_TextWrap);
        //    }
        //    Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        //}
        private void _Slot_Slider_OnSizeChanged(object? pSender, SizeChangedEventArgs pE) => _ChangeTextWrapRect();
        private void _Slot_Slider_OnLoaded(object? pSender, RoutedEventArgs e) => _ChangeTextWrapRect();

        private void _ChangeTextWrapRect()
        {
            if(!this.IsInitialized)
                return;

            var tWidth = this.SkiaCanvas.Bounds.Width * (this.Slider.Value / this.Slider.Maximum);
            this._DrawOP_TextWrap.Bounds = new Rect(10, 10, tWidth - 20, this.SkiaCanvas.Bounds.Height -  20);
        }
    }

    class DrawOP_TextWrap : ICustomDrawOperation
    {
        public Rect Bounds { get; set; }

        public void Dispose() { }

        public bool Equals(ICustomDrawOperation? other) => other == this;

        // not sure what goes here....
        public bool HitTest(Point p) { return false; }

        public void Render(ImmediateDrawingContext pContext)
        {
            var tSkia = pContext.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            using var tLease = tSkia?.Lease();
            var tSurface = tLease?.SkSurface;
            if (tSurface == null)
                return;
            PaintSurface(tSurface);
        }
        private const String TextToShow = "SkiaSharp is a cross-platform 2D graphics API " +
                                          "for .NET platforms based on Google's Skia Graphics Library. " +
                                          "It provides a comprehensive 2D API that can be used across mobile, " +
                                          "server and desktop models to render images. " +
                                          "Building SkiaSharp is mostly straight forward. " +
                                          "The main issue is the multiple dependencies for each platform. " +
                                          "However, these are easy to install as they are found on the various websites. " +
                                          "If you are just working on managed code, " +
                                          "it is even easier as there mays to skip all the native builds.";

        private void PaintSurface(SKSurface surface)
        {
            SKCanvas tCanvas = surface.Canvas;
            tCanvas.Save();
            //SKRect figureRect = tCanvas.DeviceClipBounds;

            SKRect figureRect = new SKRect((float)this.Bounds.Left, (float)this.Bounds.Top, (float)this.Bounds.Right, (float)this.Bounds.Bottom);

            float padding = 30;
            float testRectWidth = figureRect.Width - padding * 2;
            /// testRectWidth *= this.Bounds.Width;
            SKRect testRect = new(padding, padding, padding + testRectWidth, figureRect.Height - padding);

            using SKPaint paint = new() { Color = SKColors.Navy };
            tCanvas.DrawRect(figureRect, paint);

            paint.Color = SKColors.Yellow.WithAlpha(200);
            paint.IsStroke = true;
            tCanvas.DrawRect(testRect, paint);

            paint.IsStroke = false;
            paint.Color = SKColors.White;
            paint.TextSize = 16;
            paint.IsAntialias = true;
            DrawText(tCanvas, TextToShow, testRect, paint);
            tCanvas.Restore();
        }

        private static void DrawText(SKCanvas canvas, string text, SKRect rect, SKPaint paint)
        {
            float spaceWidth = paint.MeasureText(" ");
            float wordX = rect.Left;
            float wordY = rect.Top + paint.TextSize;
            foreach (string word in text.Split(' '))
            {
                float wordWidth = paint.MeasureText(word);
                if (wordWidth <= rect.Right - wordX)
                {
                    canvas.DrawText(word, wordX, wordY, paint);
                    wordX += wordWidth + spaceWidth;
                }
                else
                {
                    wordY += paint.FontSpacing;
                    wordX = rect.Left;
                }
            }
        }
    }
}