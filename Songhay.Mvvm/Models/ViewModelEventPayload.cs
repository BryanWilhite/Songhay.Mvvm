using System.ComponentModel;

namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines Prism “Event payload” for View Model operations.
    /// </summary>
    public class ViewModelEventPayload : EditorEventPayload
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is delete confirmation.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is delete confirmation; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleteConfirmation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public INotifyPropertyChanged ViewModel { get; set; }
    }
}
