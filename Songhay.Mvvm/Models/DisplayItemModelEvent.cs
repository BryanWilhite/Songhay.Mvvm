using Prism.Events;
using Songhay.Models;

namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines a Prism Event for the conventional <see cref="DisplayItemModel"/>.
    /// </summary>
    /// <seealso cref="Prism.Events.PubSubEvent{DisplayItemModel}" />
    public class DisplayItemModelEvent: PubSubEvent<DisplayItemModel>
    {
    }
}
