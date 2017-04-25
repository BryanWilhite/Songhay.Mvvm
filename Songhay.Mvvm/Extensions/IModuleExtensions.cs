using Microsoft.Practices.ServiceLocation;
using Prism.Modularity;

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
        /// <remarks>
        /// This member uses <see cref="ServiceLocator.Current.GetInstance{T}"/> which has been considered an anti-pattern.
        /// 
        /// This member was intended for “strangulation” of legacy code as an intermediate step in re-factoring.
        /// </remarks>
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
        /// <remarks>
        /// This member uses <see cref="ServiceLocator.Current.GetInstance{T}"/> which has been considered an anti-pattern.
        /// 
        /// This member was intended for “strangulation” of legacy code as an intermediate step in re-factoring.
        /// </remarks>
        public static TService GetInstance<TService>(this IModule notifier, string key)
        {
            return ServiceLocator.Current.GetInstance<TService>(key);
        }
    }
}
