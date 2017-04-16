using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="System.Windows.FrameworkElement"/>.
    /// </summary>
    public static class FrameworkElementExtensions
    {
        /// <summary>
        /// Gets the center x.
        /// </summary>
        /// <param name="element">The element.</param>
        public static double GetCenterX(this FrameworkElement element)
        {
            if (element == null) return default(double);
            return (element.Width / 2);
        }

        /// <summary>
        /// Gets the center y.
        /// </summary>
        /// <param name="element">The element.</param>
        public static double GetCenterY(this FrameworkElement element)
        {
            if (element == null) return default(double);
            return (element.Height / 2);
        }

        #if !SILVERLIGHT
        /// <summary>
        /// Gets the context menu items.
        /// </summary>
        /// <param name="element">The element.</param>
        public static IEnumerable<MenuItem> GetContextMenuItems(this FrameworkElement element)
        {
            if (element == null) return Enumerable.Empty<MenuItem>();
            if (element.ContextMenu == null) return Enumerable.Empty<MenuItem>();
            if (element.ContextMenu.Items == null) return Enumerable.Empty<MenuItem>();

            var menuItems = element.ContextMenu.Items.OfType<MenuItem>();
            return menuItems;
        }

        /// <summary>
        /// Gets the screen coordinates.
        /// </summary>
        /// <param name="element">The element.</param>
        public static Point GetScreenCoordinates(this FrameworkElement element)
        {
            return element.GetScreenCoordinates(new Point(0, 0));
        }

        /// <summary>
        /// Gets the screen coordinates.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="origin">The origin.</param>
        public static Point GetScreenCoordinates(this FrameworkElement element, Point origin)
        {
            if (element == null) return new Point();

            var point = element.PointToScreen(origin);
            return point;
        }
#endif

        /// <summary>
        /// Sets the center.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void SetCenter(this FrameworkElement element, double x, double y)
        {
            if (element == null) return;

            Canvas.SetLeft(element, x - element.GetCenterX());
            Canvas.SetTop(element, y - element.GetCenterY());
        }

        /// <summary>
        /// Sets the cursor.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="cursor">The cursor.</param>
        /// <param name="overrideAppCursor">if set to <c>true</c> override application cursor.</param>
        public static void SetCursor(this FrameworkElement element, Cursor cursor, bool overrideAppCursor = false)
        {
            if (element == null) return;

            element.Cursor = cursor;

#if !SILVERLIGHT
            //TODO: verify that Silverlight does not support Mouse.
            if (overrideAppCursor) Mouse.OverrideCursor = cursor;
#endif
        }

        /// <summary>
        /// Sets the cursor to arrow.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="overrideAppCursor">if set to <c>true</c> override application cursor.</param>
        public static void SetCursorToArrow(this FrameworkElement element, bool overrideAppCursor = false)
        {
            element.SetCursor(Cursors.Arrow, overrideAppCursor);
        }

        /// <summary>
        /// Sets the cursor to hand.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="overrideAppCursor">if set to <c>true</c> override application cursor.</param>
        public static void SetCursorToHand(this FrameworkElement element, bool overrideAppCursor = false)
        {
            element.SetCursor(Cursors.Hand, overrideAppCursor);
        }

#if !SILVERLIGHT
        /// <summary>
        /// Sets the cursor to help.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="overrideAppCursor">if set to <c>true</c> override application cursor.</param>
        public static void SetCursorToHelp(this FrameworkElement element, bool overrideAppCursor = false)
        {
            element.SetCursor(Cursors.Help, overrideAppCursor);
        }
#endif

        /// <summary>
        /// Sets the cursor to i beam.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="overrideAppCursor">if set to <c>true</c> override application cursor.</param>
        public static void SetCursorToIBeam(this FrameworkElement element, bool overrideAppCursor = false)
        {
            element.SetCursor(Cursors.IBeam, overrideAppCursor);
        }

#if !SILVERLIGHT
        /// <summary>
        /// Sets the cursor to no.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="overrideAppCursor">if set to <c>true</c> override application cursor.</param>
        public static void SetCursorToNo(this FrameworkElement element, bool overrideAppCursor = false)
        {
            element.SetCursor(Cursors.No, overrideAppCursor);
        }
#endif

        /// <summary>
        /// Sets the cursor to wait.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="overrideAppCursor">if set to <c>true</c> override application cursor.</param>
        public static void SetCursorToWait(this FrameworkElement element, bool overrideAppCursor = false)
        {
            element.SetCursor(Cursors.Wait, overrideAppCursor);
        }

        /// <summary>
        /// Sets the cursor to none.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="overrideAppCursor">if set to <c>true</c> override application cursor.</param>
        public static void SetCursorToNone(this FrameworkElement element, bool overrideAppCursor = false)
        {
            element.SetCursor(Cursors.None, overrideAppCursor);
        }

        /// <summary>
        /// Sets the left and top.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void SetLeftAndTop(this FrameworkElement element, double x, double y)
        {
            if (element == null) return;

            Canvas.SetLeft(element, x);
            Canvas.SetTop(element, y);
        }

#if !SILVERLIGHT
        /// <summary>
        /// Unsets the application cursor.
        /// </summary>
        /// <param name="element">The element.</param>
        public static void UnsetAppCursor(this FrameworkElement element)
        {
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// Unsets the cursor.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="unsetAppCursor">if set to <c>true</c> unset application cursor.</param>
        public static void UnsetCursor(this FrameworkElement element, bool unsetAppCursor = false)
        {
            if (element == null) return;

            element.Cursor = null;
            if (unsetAppCursor) element.UnsetAppCursor();
        }
#endif

        public static FrameworkElement WithValidation(this FrameworkElement frameworkElement, DependencyProperty dependencyProperty)
        {
            if (frameworkElement == null) return null;

            var expression = frameworkElement.GetBindingExpression(dependencyProperty);
            var binding = expression.ParentBinding;
            if (binding.ValidatesOnDataErrors)
            {
                expression.UpdateSource();
            }
            else
            {
                var newBinding = new Binding
                {
                    ElementName = binding.ElementName,
                    Mode = binding.Mode,
                    Path = binding.Path,
                    UpdateSourceTrigger = binding.UpdateSourceTrigger,
                    ValidatesOnDataErrors = true
                };
                frameworkElement.SetBinding(dependencyProperty, newBinding);
            }

            return frameworkElement;
        }
    }
}
