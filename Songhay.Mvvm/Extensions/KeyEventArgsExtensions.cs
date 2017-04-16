using System.Windows.Input;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="KeyEventArgs"/>
    /// </summary>
    public static class KeyEventArgsExtensions
    {
        /// <summary>
        /// Determines whether <see cref="KeyEventArgs"/> has alt.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public static bool IsAlt(this KeyEventArgs args)
        {
            if (args == null) return false;
            return (Keyboard.Modifiers == ModifierKeys.Alt);
        }

        /// <summary>
        /// Determines whether <see cref="KeyEventArgs"/> has alt with the specified key.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <param name="key">The key.</param>
        public static bool IsAlt(this KeyEventArgs args, Key key)
        {
            if (args == null) return false;
            return args.IsAlt() && (args.SystemKey == key);
        }

        /// <summary>
        /// Determines whether <see cref="KeyEventArgs"/> has control.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public static bool IsControl(this KeyEventArgs args)
        {
            if (args == null) return false;
            return (Keyboard.Modifiers == ModifierKeys.Control);
        }

        /// <summary>
        /// Determines whether <see cref="KeyEventArgs"/> has control with the specified key.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <param name="key">The key.</param>
        public static bool IsControl(this KeyEventArgs args, Key key)
        {
            if (args == null) return false;
            return args.IsControl() && (args.Key == key);
        }

        /// <summary>
        /// Determines whether <see cref="KeyEventArgs"/> has ctrl and shift.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public static bool IsControlShift(this KeyEventArgs args)
        {
            if (args == null) return false;
            return (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift));
        }

        /// <summary>
        /// Determines whether <see cref="KeyEventArgs"/> has ctrl and shift with the specified key.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <param name="key">The key.</param>
        public static bool IsControlShift(this KeyEventArgs args, Key key)
        {
            if (args == null) return false;
            return args.IsControlShift() && (args.Key == key);
        }

        /// <summary>
        /// Determines whether the specified arguments is enter.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public static bool IsEnter(this KeyEventArgs args)
        {
            if (args == null) return false;
            return (args.Key == Key.Enter);
        }

        /// <summary>
        /// Determines whether <see cref="KeyEventArgs"/> has shift.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public static bool IsShift(this KeyEventArgs args)
        {
            if (args == null) return false;
            return (Keyboard.Modifiers == ModifierKeys.Shift);
        }

        /// <summary>
        /// Determines whether <see cref="KeyEventArgs"/> has shift with the specified key.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        /// <param name="key">The key.</param>
        public static bool IsShift(this KeyEventArgs args, Key key)
        {
            if (args == null) return false;
            return args.IsShift() && (args.Key == key);
        }
    }
}
