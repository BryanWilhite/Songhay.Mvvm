﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Songhay.ValueConverters
{
    public sealed class LongToIntValueConverter : IValueConverter
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
            if (value == null) return null;

            int i;
            var test = int.TryParse(value.ToString(), out i);
            if (!test) return null;

            return i;
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
            if (value == null) return null;

            long l;
            var test = long.TryParse(value.ToString(), out l);
            if (!test) return null;

            return l;
        }

        #endregion
    }
}
