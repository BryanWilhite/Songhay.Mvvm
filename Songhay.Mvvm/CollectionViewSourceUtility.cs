using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Songhay.Extensions;

namespace Songhay
{
    /// <summary>
    /// Static members for <see cref="System.Windows.Data.CollectionViewSource"/>.
    /// </summary>
    public static class CollectionViewSourceUtility
    {
        /// <summary>
        /// Applies the groupings.
        /// </summary>
        /// <param name="viewSource">The view source.</param>
        /// <param name="groupings">The groupings.</param>
        public static void ApplyGroupings(CollectionViewSource viewSource, IEnumerable<GroupDescription> groupings)
        {
            if (!FrameworkDispatcherUtility.HasCurrentWindowsApplication()) return;
            viewSource.CheckViewSource();

            using (viewSource.DeferRefresh())
            {
                if (viewSource.GroupDescriptions.Any()) viewSource.GroupDescriptions.Clear();
                groupings.ForEachInEnumerable(i => viewSource.GroupDescriptions.Add(i));
            }
        }

        /// <summary>
        /// Applies a sort with the specified descriptions.
        /// </summary>
        /// <param name="viewSource">the <see cref="System.Windows.Data.CollectionViewSource"/></param>
        /// <param name="descriptions">the sort descriptions</param>
        public static void ApplySort(CollectionViewSource viewSource, IEnumerable<SortDescription> descriptions)
        {
            if (!FrameworkDispatcherUtility.HasCurrentWindowsApplication()) return;
            viewSource.CheckViewSource();
            if (viewSource.SortDescriptions.Any()) viewSource.SortDescriptions.Clear();
            descriptions.ForEachInEnumerable(i => viewSource.SortDescriptions.Add(i));
        }

        /// <summary>
        /// Gets the <see cref="System.Windows.Data.CollectionViewSource"/>.
        /// </summary>
        /// <param name="source">the Source</param>
        public static CollectionViewSource GetCollectionViewSource(object source)
        {
            if (!FrameworkDispatcherUtility.HasCurrentWindowsApplication()) return null;
            var viewSource = new CollectionViewSource();
            SetSource(viewSource, source);
            viewSource.CheckViewSource();
            return viewSource;
        }

        /// <summary>
        /// Gets the specified Collection View Source
        /// from the specified set with the specified key.
        /// </summary>
        /// <param name="viewSourceSet">the set of view sources</param>
        /// <param name="key">the set key</param>
        public static CollectionViewSource GetCollectionViewSource(this Dictionary<string, CollectionViewSource> viewSourceSet, string key)
        {
            if (!FrameworkDispatcherUtility.HasCurrentWindowsApplication()) return null;
            if (viewSourceSet == null) throw new NullReferenceException("The expected Collection View Source set is not here");
            if (!viewSourceSet.ContainsKey(key)) throw new NullReferenceException("The expected Collection View Source set key is not here.");
            var viewSource = viewSourceSet[key];
            return viewSource;
        }

        /// <summary>
        /// Gets the specified Collection View
        /// from the specified set with the specified key.
        /// </summary>
        /// <param name="viewSourceSet">the set of view sources</param>
        /// <param name="key">the set key</param>
        public static ICollectionView GetView(Dictionary<string, CollectionViewSource> viewSourceSet, string key)
        {
            return GetView(viewSourceSet, key, null);
        }

        /// <summary>
        /// Gets the specified Collection View
        /// from the specified set with the specified key
        /// and applies the specified Source
        /// when <see cref="System.Windows.Data.CollectionViewSource.Source"/> is <c>null</c>.
        /// </summary>
        /// <param name="viewSourceSet">the set of view sources</param>
        /// <param name="key">the set key</param>
        /// <param name="source">the Source</param>
        public static ICollectionView GetView(Dictionary<string, CollectionViewSource> viewSourceSet, string key, object source)
        {
            var viewSource = viewSourceSet.GetCollectionViewSource(key);
            return GetView(viewSource, source);
        }

        /// <summary>
        /// Gets the specified Collection View and applies the specified Source
        /// when <see cref="System.Windows.Data.CollectionViewSource.Source"/> is <c>null</c>
        /// or when <c>reapplySource</c> is <c>true</c>.
        /// </summary>
        /// <param name="viewSource">the <see cref="System.Windows.Data.CollectionViewSource"/></param>
        /// <param name="source">the Source</param>
        /// <remarks>
        /// Because <see cref="System.Windows.Data.CollectionViewSource"/> has thread affinity
        /// this extension method will “new up” an instance of <see cref="System.Windows.Data.CollectionViewSource"/>
        /// when <c>viewSource</c> is null. This attempts to ensure that the View Source is generated
        /// on the ‘getting’ thread (usually the UI thread).
        /// </remarks>
        public static ICollectionView GetView(CollectionViewSource viewSource, object source)
        {
            if (!FrameworkDispatcherUtility.HasCurrentWindowsApplication()) return null;
            if (viewSource == null) viewSource = new CollectionViewSource();
            SetSource(viewSource, source);
            return viewSource.View;
        }

        /// <summary>
        /// Sets the <see cref="System.Windows.Data.CollectionViewSource.View.Filter"/>
        /// from the specified set with the specified key.
        /// </summary>
        /// <param name="viewSourceSet">the set of view sources</param>
        /// <param name="key">the set key</param>
        /// <param name="filter">the filter of the View</param>
        public static void SetFilter(Dictionary<string, CollectionViewSource> viewSourceSet, string key, Predicate<object> filter)
        {
            var viewSource = viewSourceSet.GetCollectionViewSource(key);
            SetFilter(viewSource, filter);
        }

        /// <summary>
        /// Sets the <see cref="System.Windows.Data.CollectionViewSource.View.Filter"/>
        /// </summary>
        /// <param name="viewSource">the <see cref="System.Windows.Data.CollectionViewSource"/></param>
        /// <param name="filter">the filter of the View</param>
        public static void SetFilter(CollectionViewSource viewSource, Predicate<object> filter)
        {
            if (!FrameworkDispatcherUtility.HasCurrentWindowsApplication()) return;
            viewSource.CheckViewSource();
            viewSource.View.Filter = filter;
        }

        /// <summary>
        /// Sets the <see cref="System.Windows.Data.CollectionViewSource.Source"/>
        /// </summary>
        /// <param name="viewSource">the <see cref="System.Windows.Data.CollectionViewSource"/></param>
        /// <param name="source">the Source</param>
        /// <remarks>
        /// Memory leaks and/or orphaned objects can result from setting the Source more than once.
        /// </remarks>
        public static void SetSource(CollectionViewSource viewSource, object source)
        {
            if (!FrameworkDispatcherUtility.HasCurrentWindowsApplication()) return;
            if (viewSource == null) throw new NullReferenceException("The expected Collection View Source is not here.");
            if ((viewSource.Source == null) && (source != null)) viewSource.Source = source;
        }

        /// <summary>
        /// Verifies that the Collection View Source is initialized.
        /// </summary>
        /// <param name="viewSource">the <see cref="System.Windows.Data.CollectionViewSource"/></param>
        public static void CheckViewSource(this CollectionViewSource viewSource)
        {
            if (!FrameworkDispatcherUtility.HasCurrentWindowsApplication()) return;

            if (viewSource == null) throw new NullReferenceException("The expected Collection View Source is not here.");

            if (!FrameworkDispatcherUtility.IsCallingFromDispatcherThread())
                throw new ApplicationException("The Collection View Source is not being called from the Dispatcher thread.");

            if (viewSource.Source == null) throw new NullReferenceException("The expected Collection View Source data is not here.");

        }
    }
}
