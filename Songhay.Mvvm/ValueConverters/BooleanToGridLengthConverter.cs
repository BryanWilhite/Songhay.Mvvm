using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Songhay.ValueConverters
{
    using Songhay.Extensions;

    /// <summary>
    /// Converts <see cref="System.Boolean"/>
    /// to data in <see cref="System.Windows.GridLength"/>.
    /// </summary>
    public class BooleanToGridLengthConverter : IValueConverter
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
            var gridLength = GridLength.Auto;
            var gridLengthZero = new GridLength(0, GridUnitType.Pixel);

            if (value is bool)
            {
                flag = (bool)value;
            }
            else if (value is bool?)
            {
                var nullable = (bool?)value;
                flag = nullable.GetValueOrDefault();
            }

            if (parameter != null)
            {
                var sTrimmed = parameter.ToString().Trim();
                if (sTrimmed.EndsWith("*") && sTrimmed.Replace("*", string.Empty).IsInteger())
                {
                    gridLength = new GridLength(System.Convert.ToDouble(sTrimmed), GridUnitType.Star);
                }
                else if (sTrimmed.IsInteger())
                {
                    gridLength = new GridLength(System.Convert.ToDouble(sTrimmed), GridUnitType.Pixel);
                }
            }

            return flag ? gridLength : gridLengthZero;
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
    }
}
