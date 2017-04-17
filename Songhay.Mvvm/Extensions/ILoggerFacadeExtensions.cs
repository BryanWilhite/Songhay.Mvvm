using Prism.Logging;
using System;
using System.Windows.Input;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="ILoggerFacade"/>
    /// </summary>
    public static class ILoggerFacadeExtensions
    {
        /// <summary>
        /// Logs the key event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public static void LogKeyEvent(this ILoggerFacade logger, KeyEventArgs args)
        {
            if (args == null) return;
            if (logger == null) return;

            var s = string.Format("Ctrl?: {0}, Ctrl Shift?: {1}, Key: {2}",
                args.IsControl(), args.IsControlShift(),
                Enum.Format(typeof(Key), args.Key, "g"));
            logger.Log(s, Category.Info, Priority.Medium);
        }

        /// <summary>
        /// Logs the mouse button event.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="args">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        public static void LogMouseButtonEvent(this ILoggerFacade logger, MouseButtonEventArgs args)
        {
            if (args == null) return;
            if (logger == null) return;
            var s = string.Format("LeftButton Pressed?: {0}, RightButton Pressed?: {1}, LeftCtrl Down?:{2}, LeftShift Down?:{3}, RightCtrl Down?:{4}, RightShift Down?:{5}",
                args.LeftButton == MouseButtonState.Pressed,
                args.RightButton == MouseButtonState.Pressed,
                Keyboard.IsKeyDown(Key.LeftCtrl),
                Keyboard.IsKeyDown(Key.LeftShift),
                Keyboard.IsKeyDown(Key.RightCtrl),
                Keyboard.IsKeyDown(Key.RightShift));
            logger.Log(s, Category.Info, Priority.Medium);
        }
    }
}
