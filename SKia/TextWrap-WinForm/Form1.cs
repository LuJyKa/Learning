using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TextWrap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TrackBar_TextWrap_ValueChanged(Object sender, EventArgs e)
        {
            this.SKGLControl_Canvas.Invalidate();
        }

        private void SKGLControl_Canvas_PaintSurface(Object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            this.PaintSurface(e.Surface);
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
            SKCanvas canvas = surface.Canvas;
            SKRect figureRect = canvas.DeviceClipBounds;
            float padding = 30;
            float testRectWidth = figureRect.Width - padding * 2;
            testRectWidth *= (float)this.TrackBar_TextWrap.Value / this.TrackBar_TextWrap.Maximum;
            SKRect testRect = new(padding, padding, padding + testRectWidth, figureRect.Height - padding);

            using SKPaint paint = new() { Color = SKColors.Navy };
            canvas.DrawRect(figureRect, paint);

            paint.Color = SKColors.Yellow.WithAlpha(200);
            paint.IsStroke = true;
            canvas.DrawRect(testRect, paint);

            paint.IsStroke = false;
            paint.Color = SKColors.White;
            paint.TextSize = 16;
            paint.IsAntialias = true;
            DrawText(canvas, TextToShow, testRect, paint);
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
