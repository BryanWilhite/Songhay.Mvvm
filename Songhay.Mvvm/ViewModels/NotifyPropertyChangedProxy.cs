using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Songhay.Mvvm.ViewModels
{
    /// <summary>
    /// Dynamic wrapper, implementing <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// from https://gist.github.com/jpolvora/2730528
    /// </remarks>
    public class NotifyPropertyChangedProxy<T> : DynamicObject, INotifyPropertyChanged where T : class
    {
        /// <summary>
        /// Gets the wrapped object.
        /// </summary>
        /// <value>
        /// The wrapped object.
        /// </value>
        public T WrappedObject { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyPropertyChangedProxy{T}"/> class.
        /// </summary>
        /// <param name="wrappedObject">The wrapped object.</param>
        /// <exception cref="System.ArgumentNullException">wrappedObject</exception>
        public NotifyPropertyChangedProxy(T wrappedObject)
        {
            if (wrappedObject == null)
                throw new ArgumentNullException("wrappedObject");

            WrappedObject = wrappedObject;
        }

        #region INotifyPropertyChanged support

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Returns the enumeration of all dynamic member names.
        /// </summary>
        /// <returns>
        /// A sequence that contains dynamic member names.
        /// </returns>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            if (!_allCached)
            {
                var properties = WrappedObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var property in properties.Where(property => !_cache.ContainsKey(property.Name)))
                {
                    _cache[property.Name] = property;
                }
            }
            _allCached = true;
            return _cache.Keys;
        }

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // Locate property by name

            PropertyInfo propertyInfo;
            if (!_cache.TryGetValue(binder.Name, out propertyInfo))
            {
                propertyInfo = WrappedObject.GetType().GetProperty(binder.Name, BindingFlags.Instance | BindingFlags.Public | (binder.IgnoreCase ? BindingFlags.IgnoreCase : 0));
            }

            if (propertyInfo == null || !propertyInfo.CanRead)
            {
                result = null;
                return false;
            }

            result = propertyInfo.GetValue(WrappedObject, null);
            return true;
        }

        /// <summary>
        /// Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
        /// </returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            // Locate property by name

            PropertyInfo propertyInfo;
            if (!_cache.TryGetValue(binder.Name, out propertyInfo))
            {
                propertyInfo = WrappedObject.GetType().GetProperty(binder.Name, BindingFlags.Instance | BindingFlags.Public | (binder.IgnoreCase ? BindingFlags.IgnoreCase : 0));
            }

            if (propertyInfo == null || !propertyInfo.CanWrite)
                return false;

            object newValue = value;

            // Check the types are compatible
            Type propertyType = propertyInfo.PropertyType;
            if (!propertyType.IsAssignableFrom(value.GetType()))
            {
                newValue = Convert.ChangeType(value, propertyType);
            }

            propertyInfo.SetValue(WrappedObject, newValue, null);
            OnPropertyChanged(binder.Name);
            return true;
        }

        bool _allCached;
        readonly Dictionary<string, PropertyInfo> _cache = new Dictionary<string, PropertyInfo>();
    }
}
