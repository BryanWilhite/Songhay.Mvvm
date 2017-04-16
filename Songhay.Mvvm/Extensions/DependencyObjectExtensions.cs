using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Songhay.Mvvm.Extensions
{
    /// <summary>
    /// Extensions for <see cref="System.Windows.DependencyObject"/>.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Activate the conventional View (<see cref="UserControl"/>).
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="viewContainer">The view container.</param>
        /// <param name="qualifiedViewName">Name of the qualified view.</param>
        /// <remarks>
        /// Activating views allows views to be pre-loaded and stored in a lookup (Dictionary).
        /// This is a lightweight alternative to formal PRISM Navigation.
        ///
        /// The “qualified” View name should start with the root namespace,
        /// the convential “Views” qualifier and the view name:
        /// <c>MyApp.Views.MyView</c>
        /// </remarks>
        public static UserControl ActivateView(this DependencyObject dependencyObject, Assembly viewContainer, string qualifiedViewName)
        {
            var handle = Activator.CreateInstanceFrom(viewContainer.CodeBase, qualifiedViewName);
            return (UserControl)handle.Unwrap();
        }

        /// <summary>
        /// Walk up the VisualTree, returning first parent object of the type supplied as type parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <remarks>
        /// For more, see “Expand/Collapse button in a Silverlight DataGrid”
        /// [http://stackoverflow.com/questions/5232683/expand-collapse-button-in-a-silverlight-datagrid]
        /// </remarks>
        public static T FindAncestor<T>(this DependencyObject obj) where T : DependencyObject
        {
            while (obj != null)
            {
                T o = obj as T;
                if (o != null)
                    return o;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="property">The property.</param>
        public static double? GetDouble(this DependencyObject dependencyObject, DependencyProperty property)
        {
            if (dependencyObject == null) return null;
            return (double)dependencyObject.GetValue(property);
        }

        /// <summary>
        /// Increments the double.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="property">The property.</param>
        /// <param name="increment">The increment.</param>
        public static void IncrementDouble(this DependencyObject dependencyObject, DependencyProperty property, double increment)
        {
            var currentValue = dependencyObject.GetDouble(property);
            if (currentValue == null) return;
            dependencyObject.SetDouble(property, currentValue.GetValueOrDefault() + increment);
        }

        /// <summary>
        /// Sets the double.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        public static void SetDouble(this DependencyObject dependencyObject, DependencyProperty property, double value)
        {
            if (dependencyObject == null) return;
            dependencyObject.SetValue(property, value);
        }
    }
}
