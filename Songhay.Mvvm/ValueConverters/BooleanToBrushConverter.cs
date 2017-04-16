using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace Songhay.ValueConverters
{
    using Songhay.Extensions;
    using Mvvm.Extensions;

    /// <summary>
    /// Converts <see cref="System.Boolean"/>
    /// to <see cref="System.Windows.Media.SolidColorBrush"/>.
    /// </summary>
    public class BooleanToBrushConverter : IValueConverter
    {
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type" /> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = false;

            if (value is bool)
            {
                flag = (bool)value;
            }
            else if (value is bool?)
            {
                var nullable = (bool?)value;
                flag = nullable.GetValueOrDefault();
            }
            else if ((value != null))
            {
                var s = FrameworkTypeUtility.ParseString(value);
                flag = s.IsInteger(i => i > 0);
            }

            var brushes = this.GetBrushes(parameter);
            return flag ? brushes.First() : brushes.Last();

        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay" /> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type" /> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        IEnumerable<SolidColorBrush> GetBrushes(object parameter)
        {
            if (parameter == null) throw new NullReferenceException("The expected parameter is not here.");
            var brushes = parameter.ToString().Split(',').Select(i => new SolidColorBrush(i.ToColor()));
            return brushes;
        }
    }
}
