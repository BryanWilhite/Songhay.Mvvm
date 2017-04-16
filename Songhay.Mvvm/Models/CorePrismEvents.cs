using Microsoft.Practices.Prism.PubSubEvents;
using Songhay.Models;

namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines core Prism “events” 
    /// </summary>
    public static class CorePrismEvents
    {
        /// <summary>
        /// The data service context event
        /// </summary>
        public static readonly PubSubEvent<DataServiceContextEventPayload> DataServiceContextEvent = new PubSubEvent<DataServiceContextEventPayload>();

        /// <summary>
        /// The display item model event
        /// </summary>
        public static readonly PubSubEvent<DisplayItemModel> DisplayItemModelEvent = new PubSubEvent<DisplayItemModel>();

        /// <summary>
        /// The key input event
        /// </summary>
        public static readonly PubSubEvent<EventPayload> KeyInputEvent = new PubSubEvent<EventPayload>();

        /// <summary>
        /// The menu display item model event
        /// </summary>
        public static readonly PubSubEvent<MenuDisplayItemModel> MenuDisplayItemModelEvent = new PubSubEvent<MenuDisplayItemModel>();

        /// <summary>
        /// The model event for view model
        /// </summary>
        public static readonly PubSubEvent<ViewModelEventPayload> ViewModelEvent = new PubSubEvent<ViewModelEventPayload>();
    }
}
