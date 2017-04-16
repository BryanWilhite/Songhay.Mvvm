using System.Windows.Media;
using Songhay.Models;

namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Extends <see cref="DisplayItemModel"/>
    /// with high-performance, solid-color brushes.
    /// </summary>
    /// <remarks>
    /// This class should be useful for XAML grids with a large number of rows.
    /// </remarks>
    public class DisplayItemModelWithBrushes : DisplayItemModel
    {
        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        /// <value>
        /// The background brush.
        /// </value>
        public Brush BackgroundBrush { get; set; }

        /// <summary>
        /// Gets or sets the foreground brush.
        /// </summary>
        /// <value>
        /// The foreground brush.
        /// </value>
        public Brush ForegroundBrush { get; set; }
    }
}
