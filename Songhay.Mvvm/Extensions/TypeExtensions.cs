using System;
using System.Globalization;
using System.Reflection;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="System.Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the default view type for the Prism View Model type resolver.
        /// </summary>
        /// <param name="viewType">Type of the view.</param>
        /// <returns></returns>
        public static Type GetDefaultViewTypeForViewModelTypeResolver(this Type viewType)
        {
            var viewName = viewType.FullName;
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}",
                viewName.Replace("View", "ViewModel").Replace("Page", "ViewModel"),
                viewAssemblyName);
            return Type.GetType(viewModelName);
        }
    }
}
