using Prism.Events;

namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines a Prism Event for the conventional View Model
    /// with payload <see cref="ViewModelEventPayload"/>.
    /// </summary>
    /// <seealso cref="PubSubEvent{ViewModelEventPayload}" />
    public class ViewModelEvent : PubSubEvent<ViewModelEventPayload>
    {
    }
}
