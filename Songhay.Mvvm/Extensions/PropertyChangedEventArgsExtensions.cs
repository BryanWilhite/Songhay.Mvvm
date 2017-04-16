using System.ComponentModel;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="PropertyChangedEventArgs"/>
    /// </summary>
    public static class PropertyChangedEventArgsExtensions
    {
        /// <summary>
        /// Determines whether <see cref="PropertyChangedEventArgs.PropertyName"/>
        /// is <see cref="IDeletable.IsDeleted"/>.
        /// </summary>
        /// <param name="args">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        public static bool IsPropertyIsDeleted(this PropertyChangedEventArgs args)
        {
            if (args == null) return false;
            return (args.PropertyName == "IsDeleted");
        }

        /// <summary>
        /// Determines whether <see cref="PropertyChangedEventArgs.PropertyName"/>
        /// is <see cref="IDeletable.IsMarkedForDeletion"/>.
        /// </summary>
        /// <param name="args">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        public static bool IsPropertyIsMarkedForDeletion(this PropertyChangedEventArgs args)
        {
            if (args == null) return false;
            return (args.PropertyName == "IsMarkedForDeletion");
        }

        /// <summary>
        /// Determines whether <see cref="PropertyChangedEventArgs.PropertyName"/>
        /// is <c>IsModified</c>.
        /// </summary>
        /// <param name="args">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        public static bool IsPropertyIsModified(this PropertyChangedEventArgs args)
        {
            if (args == null) return false;
            return (args.PropertyName == "IsModified");
        }
    }
}
