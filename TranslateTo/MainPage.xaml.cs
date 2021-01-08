using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace TranslateTo
{
    public partial class MainPage : ContentPage
    {
        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            viewportHeight = info.Height;
            viewportWidth = info.Width;
        }

        public async void OnTouchAction(object sender, SKTouchEventArgs e)
        {
            e.Handled = true;
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    await movingView.ScaleTo(0.5, 0);
                    break;
                case SKTouchAction.Moved:
                    var location = e.Location;
                    var x = location.X * container.Width / viewportWidth;
                    var y = location.Y * container.Height / viewportHeight;
                    Console.WriteLine($"X={x}/{container.Width} Y={x}/{container.Height}");
                    await movingView.TranslateTo(x, y, 0);
                    break;
                case SKTouchAction.Released:
                    await movingView.TranslateTo(0, 0, 0);
                    await movingView.ScaleTo(1, 0);
                    break;
            }
        }

        private int viewportHeight;
        private int viewportWidth;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
