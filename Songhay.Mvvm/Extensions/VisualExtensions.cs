using System;
using System.Windows;
using System.Windows.Media;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="Visual"/>
    /// </summary>
    public static class VisualExtensions
    {
        /// <summary>
        /// Gets the visual origin.
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <param name="ancestorVisual">The ancestor visual.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">ancestorVisual;The expected ancestor visual is not here.</exception>
        public static Point GetVisualOrigin(this Visual visual, Visual ancestorVisual)
        {
            if (visual == null) return default(Point);
            if (ancestorVisual == null) throw new ArgumentNullException("ancestorVisual", "The expected ancestor visual is not here.");

            var transform = visual.TransformToAncestor(ancestorVisual);
            var point = transform.Transform(new Point(0, 0));
            return point;
        }
    }
}
