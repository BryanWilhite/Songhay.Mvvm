using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Songhay.ValueConverters
{
    /// <summary>
    /// Converts <see cref="System.Boolean"/> source data
    /// into <see cref="System.String"/>.
    /// </summary>
    public sealed class BooleanToTextConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Modifies the source data before passing it to the target for targetValues in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (parameter == null) parameter = "Yes,No";
            var targetValues = parameter.ToString().Split(',');
            if (targetValues.Length < 2) throw new ArgumentException("The parameter is not in the expected format.");
            return (bool)value ? targetValues[0] : targetValues[1];
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var s = (string)value;

            if (parameter == null) parameter = "Yes,No";
            var targetValues = parameter.ToString().Split(',');
            if (targetValues.Length < 2) throw new ArgumentException("The parameter is not in the expected format.");
            if (string.IsNullOrEmpty(s)) return DependencyProperty.UnsetValue;
            return (s.Equals((string)targetValues[0]));
        }

        #endregion
    }
}
