using Prism.Events;

namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines a Prism Event for keyboard input with <see cref="EventPayload"/>.
    /// </summary>
    /// <seealso cref="PubSubEvent{EventPayload}" />
    public class KeyInputEvent: PubSubEvent<EventPayload>
    {
    }
}
