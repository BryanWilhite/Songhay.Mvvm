using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="System.Windows.Media.Color"/>.
    /// </summary>
    public static partial class ColorExtensions
    {
        /// <summary>
        /// Converts the specified pair into a key-<see cref="System.Windows.Media.SolidColorBrush"/>-value pair.
        /// </summary>
        /// <param name="pair">The pair.</param>
        public static IEnumerable<KeyValuePair<string, SolidColorBrush>> ToPairsWithBrushes(this IEnumerable<KeyValuePair<string, string>> pairs)
        {
            if (pairs == null) return Enumerable.Empty<KeyValuePair<string, SolidColorBrush>>();
            return pairs.Select(i => i.ToPairWithBrush());
        }

        /// <summary>
        /// Converts the specified pair into a key-<see cref="System.Windows.Media.SolidColorBrush"/>-value pair.
        /// </summary>
        /// <param name="pair">The pair.</param>
        public static KeyValuePair<string, SolidColorBrush> ToPairWithBrush(this KeyValuePair<string, string> pair)
        {
            var pairOut = new KeyValuePair<string, SolidColorBrush>(pair.Key, pair.Value.ToBrush());
            return pairOut;
        }

        /// <summary>
        /// Converts the current value into <see cref="System.Windows.Media.SolidColorBrush"/>.
        /// </summary>
        /// <param name="hexColor">Color of the hex.</param>
        public static SolidColorBrush ToBrush(this string hexColor)
        {
            var color = hexColor.ToColor();
            return new SolidColorBrush(color);
        }

        /// <summary>
        /// Converts the current value into <see cref="System.Windows.Media.Color"/>.
        /// </summary>
        /// <param name="hexColor">Color of the hex.</param>
        public static Color ToColor(this string hexColor)
        {
            if (string.IsNullOrEmpty(hexColor)) return default(Color);
            uint argb = UInt32.Parse(hexColor.Replace("#", string.Empty), NumberStyles.HexNumber);
            return argb.ToColor();
        }

        /// <summary>
        /// Converts the current value into <see cref="System.Windows.Media.Color"/>.
        /// </summary>
        /// <param name="argb">The ARGB.</param>
        public static Color ToColor(this uint argb)
        {
            return Color.FromArgb(
                (byte)((argb & -16777216) >> 0x18),
                (byte)((argb & 0xff0000) >> 0x10),
                (byte)((argb & 0xff00) >> 8),
                (byte)(argb & 0xff));
        }
    }
}
