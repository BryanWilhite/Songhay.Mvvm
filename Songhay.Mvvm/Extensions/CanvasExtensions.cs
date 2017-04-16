using System;
using System.Windows;
using System.Windows.Controls;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="Canvas"/>.
    /// </summary>
    public static class CanvasExtensions
    {
        /// <summary>
        /// Gets the center.
        /// </summary>
        /// <param name="element">The element.</param>
        public static Point GetCenter(this FrameworkElement element)
        {
            if (element == null) return default(Point);
            var x = Canvas.GetLeft(element);
            var y = Canvas.GetTop(element);
            return new Point(x - element.GetCenterX(), (y - element.GetCenterY()));
        }

        /// <summary>
        /// Simulates the scroll down.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="childPanel">The child panel.</param>
        /// <param name="increment">The increment.</param>
        /// <param name="durationInMilliseconds">The duration in milliseconds.</param>
        public static void SimulateScrollDown(this Canvas canvas, Panel childPanel, double increment, double durationInMilliseconds)
        {
            if (canvas == null) return;
            if (childPanel == null) return;

            var currentTop = childPanel.GetDouble(Canvas.TopProperty);
            if (currentTop == null) return;

            if ((Math.Abs(currentTop.GetValueOrDefault()) + increment) > childPanel.ActualHeight)
            {
                childPanel.SetDouble(Canvas.TopProperty, 0);
            }
            else childPanel.AnimatePropertyByIncrement(Canvas.TopProperty, -increment, durationInMilliseconds);
        }

        /// <summary>
        /// Simulates the scroll up.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="childPanel">The child panel.</param>
        /// <param name="increment">The increment.</param>
        /// <param name="durationInMilliseconds">The duration in milliseconds.</param>
        public static void SimulateScrollUp(this Canvas canvas, Panel childPanel, double increment, double durationInMilliseconds)
        {
            if (canvas == null) return;
            if (childPanel == null) return;

            var currentTop = childPanel.GetDouble(Canvas.TopProperty);
            if (currentTop == null) return;

            if (currentTop.GetValueOrDefault() != 0) childPanel.AnimatePropertyByIncrement(Canvas.TopProperty, increment, durationInMilliseconds);
        }
    }
}
