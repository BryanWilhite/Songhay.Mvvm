using Prism.Events;
using Songhay.Models;

namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines a Prism Event for the conventional <see cref="MenuDisplayItemModel"/>.
    /// </summary>
    /// <seealso cref="PubSubEvent{MenuDisplayItemModel}" />
    public class MenuDisplayItemModelEvent: PubSubEvent<MenuDisplayItemModel>
    {
    }
}
