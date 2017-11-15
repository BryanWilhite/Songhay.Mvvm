using Prism.Mvvm;
using Prism.Regions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="BindableBase"/>.
    /// </summary>
    public static class BindableBaseExtensions
    {
        /// <summary>
        /// Gets from navigation context.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="navigationContext">The navigation context.</param>
        /// <param name="key">The key.</param>
        public static object GetFromNavigationContext(this BindableBase viewModel, NavigationContext navigationContext, string key)
        {
            if (navigationContext == null) return null;
            if (!navigationContext.Parameters.Any()) return null;

            var o = navigationContext.Parameters.FirstOrDefault(i => i.Key == key);
            return o;
        }

        /// <summary>
        /// Gets the identifier parameter.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="navigationContext">The navigation context.</param>
        public static int? GetIdParameter(this BindableBase viewModel, NavigationContext navigationContext)
        {
            if (navigationContext == null) return null;
            if (!navigationContext.Parameters.Any()) return null;

            var s = navigationContext.Parameters["id"];
            if (s == null) return null;

            var id = Convert.ToInt32(s);
            return id;
        }

        /// <summary>
        /// Determines whether [is in design mode] [the specified view model].
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <remarks>
        /// From [http://stackoverflow.com/questions/3757646/how-do-i-stop-my-viewmodel-code-from-running-in-the-designer]
        /// </remarks>
        public static bool IsInDesignMode(this BindableBase viewModel)
        {
            var test = false;
#if DEBUG
            test = (DesignerProperties.GetIsInDesignMode(new DependencyObject()));
#endif
            return test;
        }

        /// <summary>
        /// Determines whether the specified View Model is the Prism Navigation target.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="navigationContext">The navigation context.</param>
        public static bool IsINavigationAwareNavigationTarget(this BindableBase viewModel, NavigationContext navigationContext)
        {
            if (viewModel == null) return false;
            if (navigationContext == null) return false;

            var key = viewModel.GetType().Name.Replace("Model", string.Empty);
            return navigationContext.Uri.OriginalString.Contains(key);
        }
    }
}
