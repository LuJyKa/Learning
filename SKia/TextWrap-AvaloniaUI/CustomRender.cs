﻿using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using Avalonia;
using SkiaSharp;
using System.Diagnostics;
using System.Linq;
using System;
using Avalonia.Platform;

namespace TextWrap;

public class CustomSkiaPage : Control
{
    private readonly GlyphRun _noSkia;
    public CustomSkiaPage()
    {
        ClipToBounds = true;
        var text = "Current rendering API is not Skia";
        var glyphs = text.Select(ch => Typeface.Default.GlyphTypeface.GetGlyph(ch)).ToArray();
        _noSkia = new GlyphRun(Typeface.Default.GlyphTypeface, 12, text.AsMemory(), glyphs);
        _OP = new CustomDrawOp(_noSkia);
    }

    class CustomDrawOp : ICustomDrawOperation
    {
        private readonly IImmutableGlyphRunReference _noSkia;

        public CustomDrawOp(GlyphRun noSkia)
        {
            _noSkia = noSkia.TryCreateImmutableGlyphRunReference();
        }

        public void Dispose()
        {
            // No-op
        }

        public Rect Bounds { get; set; }
        public bool HitTest(Point p) => false;
        public bool Equals(ICustomDrawOperation other) => false;
        static Stopwatch St = Stopwatch.StartNew();
        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature == null)
                context.DrawGlyphRun(Brushes.Black, _noSkia);
            else
            {
                using var lease = leaseFeature.Lease();
                var canvas = lease.SkCanvas;
                canvas.Save();
                // create the first shader
                var colors = new SKColor[] {
                        new SKColor(0, 255, 255),
                        new SKColor(255, 0, 255),
                        new SKColor(255, 255, 0),
                        new SKColor(0, 255, 255)
                    };

                var sx = Animate(100, 2, 10);
                var sy = Animate(1000, 5, 15);
                var lightPosition = new SKPoint(
                    (float)(Bounds.Width / 2 + Math.Cos(St.Elapsed.TotalSeconds) * Bounds.Width / 4),
                    (float)(Bounds.Height / 2 + Math.Sin(St.Elapsed.TotalSeconds) * Bounds.Height / 4));
                using (var sweep =
                    SKShader.CreateSweepGradient(new SKPoint((int)Bounds.Width / 2, (int)Bounds.Height / 2), colors,
                        null))
                using (var turbulence = SKShader.CreatePerlinNoiseFractalNoise(0.05f, 0.05f, 4, 0))
                using (var shader = SKShader.CreateCompose(sweep, turbulence, SKBlendMode.SrcATop))
                using (var blur = SKImageFilter.CreateBlur(Animate(100, 2, 10), Animate(100, 5, 15)))
                using (var paint = new SKPaint
                {
                    Shader = shader,
                    ImageFilter = blur
                })
                    canvas.DrawPaint(paint);

                using (var pseudoLight = SKShader.CreateRadialGradient(
                    lightPosition,
                    (float)(Bounds.Width / 3),
                    new[] {
                            new SKColor(255, 200, 200, 100),
                            SKColors.Transparent,
                            new SKColor(40,40,40, 220),
                            new SKColor(20,20,20, (byte)Animate(100, 200,220)) },
                    new float[] { 0.3f, 0.3f, 0.8f, 1 },
                    SKShaderTileMode.Clamp))
                using (var paint = new SKPaint
                {
                    Shader = pseudoLight
                })
                    canvas.DrawPaint(paint);
                canvas.Restore();
            }
        }
        static int Animate(int d, int from, int to)
        {
            var ms = (int)(St.ElapsedMilliseconds / d);
            var diff = to - from;
            var range = diff * 2;
            var v = ms % range;
            if (v > diff)
                v = range - v;
            var rv = v + from;
            if (rv < from || rv > to)
                throw new Exception("WTF");
            return rv;
        }
    }

    private CustomDrawOp _OP;

    public override void Render(DrawingContext context)
    {
        _OP.Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height);
        context.Custom(_OP);
        Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
    }
}

