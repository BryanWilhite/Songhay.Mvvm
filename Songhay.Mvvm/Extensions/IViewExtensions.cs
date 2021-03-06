﻿using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using Songhay.Mvvm.Models;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="IView"/>
    /// for Prism, etc.
    /// </summary>
    public static class IViewExtensions
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="notifier">The notifier.</param>
        /// <remarks>
        /// This member uses <see cref="ServiceLocator.Current.GetInstance{T}"/> which has been considered an anti-pattern.
        /// </remarks>
        public static TService GetInstance<TService>(this IView notifier)
        {
            return ServiceLocator.Current.GetInstance<TService>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="notifier">The notifier.</param>
        /// <param name="key">The key.</param>
        /// <remarks>
        /// This member uses <see cref="ServiceLocator.Current.GetInstance{T}"/> which has been considered an anti-pattern.
        /// </remarks>
        public static TService GetInstance<TService>(this IView notifier, string key)
        {
            return ServiceLocator.Current.GetInstance<TService>(key);
        }

        /// <summary>
        /// Determines whether [is i navigation aware navigation target] [the specified view].
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="navigationContext">The navigation context.</param>
        public static bool IsINavigationAwareNavigationTarget(this IView view, NavigationContext navigationContext)
        {
            if (view == null) return false;
            if (navigationContext == null) return false;

            var key = view.GetType().Name;
            return navigationContext.Uri.OriginalString.Contains(key);
        }
    }
}
