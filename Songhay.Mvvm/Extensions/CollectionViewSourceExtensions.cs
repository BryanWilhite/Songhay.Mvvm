using System.ComponentModel;
using System.Windows.Data;

namespace Songhay.Mvvm.Extensions
{
    public static class CollectionViewSourceExtensions
    {
        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="viewSource">The view source.</param>
        /// <param name="source">The source.</param>
        public static ICollectionView GetView(this CollectionViewSource viewSource, object source)
        {
            return viewSource.GetView(source, false);
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="viewSource">The view source.</param>
        /// <param name="source">The source.</param>
        /// <param name="reapplySource">if set to <c>true</c> reapply source.</param>
        /// <remarks>
        /// The <c>source</c> is usually an Observable Collection:
        /// <code>
        ///     public ICollectionView MyCollectionView
        ///     {
        ///         get { return this._myViewSource.GetView(this.MyCollection); }
        ///     }
        /// </code>
        /// </remarks>
        public static ICollectionView GetView(this CollectionViewSource viewSource, object source, bool reapplySource)
        {
            if (viewSource == null) return null;
            if ((viewSource.Source == null) || reapplySource) viewSource.Source = source;
            return viewSource.View;
        }
    }
}
