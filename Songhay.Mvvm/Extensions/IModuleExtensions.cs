using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="IModule"/>
    /// for Prism, etc.
    /// </summary>
    public static class IModuleExtensions
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="notifier">The notifier.</param>
        public static TService GetInstance<TService>(this IModule notifier)
        {
            return ServiceLocator.Current.GetInstance<TService>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="notifier">The notifier.</param>
        /// <param name="key">The key.</param>
        public static TService GetInstance<TService>(this IModule notifier, string key)
        {
            return ServiceLocator.Current.GetInstance<TService>(key);
        }
    }
}
