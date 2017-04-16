using System.Windows.Input;

namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines Prism “Event payload”.
    /// </summary>
    public class EventPayload
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is key.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is key; otherwise, <c>false</c>.
        /// </value>
        public bool IsKey { get; set; }

        /// <summary>
        /// Gets or sets the key arguments.
        /// </summary>
        /// <value>
        /// The key arguments.
        /// </value>
        public KeyEventArgs KeyArgs { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public string Tag { get; set; }
    }
}
