using Prism.Events;

namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines a Prism Event for the conventional Data Service Context
    /// with <see cref="DataServiceContextEventPayload"/>.
    /// </summary>
    /// <seealso cref="PubSubEvent{DataServiceContextEventPayload}" />
    public class DataServiceContextEvent: PubSubEvent<DataServiceContextEventPayload>
    {
    }
}
