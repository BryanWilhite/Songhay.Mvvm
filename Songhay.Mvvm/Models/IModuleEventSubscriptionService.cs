namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines a wrapper for Module-level Prism events.
    /// </summary>
    /// <remarks>
    /// Prism 6.x by default will destroy its <see cref="IModule"/> types
    /// after they are initialized. It follows that event subscriptions
    /// should not be there.
    /// </remarks>
    public interface IModuleEventSubscriptionService
    {

        /// <summary>
        /// Sets up Prism eventing.
        /// </summary>
        void SetupPrismEventing();
    }
}
