
namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines Prism “Event payload” for Data Service Context.
    /// </summary>
    public class DataServiceContextEventPayload : EventPayload
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        /// <value>
        /// The display text.
        /// </value>
        public string DisplayText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has saved changes.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has saved changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasSavedChanges { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is unavailable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is unavailable; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnavailable { get; set; }
    }
}
