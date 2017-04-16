
namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines Prism “Event payload” for editor CRUD operations.
    /// </summary>
    /// <typeparam name="TModel">The type of the entity model.</typeparam>
    public class EditorEventPayload : EventPayload
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is delete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is delete; otherwise, <c>false</c>.
        /// </value>
        public bool IsDelete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is edit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is edit; otherwise, <c>false</c>.
        /// </value>
        public bool IsEdit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is insert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is insert; otherwise, <c>false</c>.
        /// </value>
        public bool IsInsert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is update.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is update; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpdate { get; set; }

        public override string ToString()
        {
            var s = string.Format("IsDelete: {0}, IsEdit: {1}, IsInsert: {2}, IsUpdate: {3}", this.IsDelete, this.IsEdit, this.IsInsert, this.IsUpdate);
            return s;
        }
    }
}
