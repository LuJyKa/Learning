using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Threading;
using System;

namespace TextWrap;

public class SkiaCanvas : Control
{
    public ICustomDrawOperation? DrawOP { get; set; }
    public SkiaCanvas()
    {
        /// Bounds = new Rect(0, 0, this.Width, this.Height);

        /// HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
        /// VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

        Initialized += SkiaCanvas_Initialized;

    }
    public Boolean IsAlwaysRefresh { get; set; } = true;
    private void SkiaCanvas_Initialized(object? sender, EventArgs e)
    {
        // Remove this if you don't need to do anything when this event is raised.
    }

    public override void Render(DrawingContext pContext)
    {
        base.Render(pContext);
        if (DrawOP != null)
            pContext.Custom(DrawOP);

        if(IsAlwaysRefresh)
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
    }
}

