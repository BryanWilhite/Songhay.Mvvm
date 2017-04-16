
namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Formally defines that a View Model can be deleted outright
    /// or marked for deletion via property changed notifications.
    /// </summary>
    public interface IDeletable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is marked for deletion.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is marked for deletion; otherwise, <c>false</c>.
        /// </value>
        bool IsMarkedForDeletion { get; set; }
    }
}
