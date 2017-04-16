using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions of <see cref="UIElement"/>.
    /// </summary>
    public static class UIElementExtensions
    {
        /// <summary>
        /// Animates the property.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="property">The property.</param>
        /// <param name="toValue">To value.</param>
        /// <param name="durationInMilliseconds">The duration in milliseconds.</param>
        public static void AnimateProperty(this UIElement element, DependencyProperty property, double toValue, double durationInMilliseconds)
        {
            if (element == null) return;

            var duration = new Duration(TimeSpan.FromMilliseconds(durationInMilliseconds));
            var animation = new DoubleAnimation(toValue, duration);
            element.BeginAnimation(property, animation);
        }

        /// <summary>
        /// Animates the property by increment.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="property">The property.</param>
        /// <param name="increment">The increment.</param>
        /// <param name="durationInMilliseconds">The duration in milliseconds.</param>
        public static void AnimatePropertyByIncrement(this UIElement element, DependencyProperty property, double increment, double durationInMilliseconds)
        {
            if (element == null) return;

            var currentPropertyValue = element.GetDouble(property);
            if (currentPropertyValue == null) return;

            element.AnimateProperty(property, currentPropertyValue.GetValueOrDefault() + increment, durationInMilliseconds);
        }

        /// <summary>
        /// Removes all animation clocks from property.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="property">The property.</param>
        public static void RemoveAllAnimationClocksFromProperty(this UIElement element, DependencyProperty property)
        {
            if (element == null) return;
            element.BeginAnimation(property, null);
        }
    }
}
