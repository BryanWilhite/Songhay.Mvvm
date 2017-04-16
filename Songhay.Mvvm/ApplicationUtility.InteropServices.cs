using System;
using System.Runtime.InteropServices;

namespace Songhay.Mvvm
{
    /// <summary>
    /// Interop POINT structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="POINT"/> struct.
        /// </summary>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        public POINT(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// The x
        /// </summary>
        public int X;

        /// <summary>
        /// The y
        /// </summary>
        public int Y;
    }

    /// <summary>
    /// Static members for WPF Applications
    /// </summary>
    public static partial class ApplicationUtility
    {
        /// <summary>
        /// Moves the cursor.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void MoveCursor(int x = 0, int y = 0)
        {
            POINT lpPoint;
            if (!GetCursorPos(out lpPoint)) return;

            x += lpPoint.X;
            y += lpPoint.Y;

            SetCursorPos(x, y);
        }

        /// <summary>
        /// Sets the cursor position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void SetCursorPosition(double x, double y)
        {
            SetCursorPos(Convert.ToInt32(x), Convert.ToInt32(y));
        }

        /// <summary>
        /// Sets the cursor position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint); //ref: [http://pinvoke.net/default.aspx/user32.GetCursorPos]

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
    }
}
