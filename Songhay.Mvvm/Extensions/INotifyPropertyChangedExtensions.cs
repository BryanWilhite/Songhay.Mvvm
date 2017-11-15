using Microsoft.Practices.ServiceLocation;
using System;
using System.ComponentModel;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="INotifyPropertyChanged"/>
    /// for Prism, etc.
    /// </summary>
    public static class INotifyPropertyChangedExtensions
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="notifier">The notifier.</param>
        /// <remarks>
        /// This member uses <see cref="ServiceLocator.Current.GetInstance{T}"/> which has been considered an anti-pattern.
        ///
        /// This member was intended for “strangulation” of legacy code as an intermediate step in re-factoring.
        /// </remarks>
        public static TService GetInstance<TService>(this INotifyPropertyChanged notifier)
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
        ///
        /// This member was intended for “strangulation” of legacy code as an intermediate step in re-factoring.
        /// </remarks>
        public static TService GetInstance<TService>(this INotifyPropertyChanged notifier, string key)
        {
            return ServiceLocator.Current.GetInstance<TService>(key);
        }

        /// <summary>
        /// Updates the model.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="notifier">The notifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <exception cref="System.NullReferenceException">Notifier as TViewModel returns null.</exception>
        public static void UpdateModel<TViewModel, TModel>(this INotifyPropertyChanged notifier, TModel model, string propertyName)
            where TViewModel : class
            where TModel : class, TViewModel
        {
            if (notifier == null) return;
            var vm = notifier as TViewModel;
            if (vm == null) throw new NullReferenceException("Notifier as TViewModel returns null.");
            FrameworkTypeUtility.SetProperties<TViewModel, TModel>(vm, model, new[] { propertyName });
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="notifier">The notifier.</param>
        /// <param name="model">The model.</param>
        /// <exception cref="System.NullReferenceException">Notifier as TViewModel returns null.</exception>
        public static void UpdateViewModel<TModel, TViewModel>(this INotifyPropertyChanged notifier, TModel model)
            where TViewModel : class
            where TModel : class, TViewModel
        {
            if (notifier == null) return;
            var vm = notifier as TViewModel;
            if (vm == null) throw new NullReferenceException("Notifier as TViewModel returns null.");
            FrameworkTypeUtility.SetProperties<TModel, TViewModel>(model, vm);
        }
    }
}
