using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Songhay.Mvvm.ViewModels
{
    /// <summary>
    /// Defines View Model validation for <see cref="BindableBase"/>.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Mvvm.BindableBase" />
    /// <seealso cref="System.ComponentModel.INotifyDataErrorInfo" />
    public abstract class BindableBaseWithValidation : BindableBase, INotifyDataErrorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindableBaseWithValidation"/> class.
        /// </summary>
        public BindableBaseWithValidation()
        {
            this._validationErrors = new Dictionary<string, List<string>>();
            this.IsViewModelDataValid = true;
        }

        #region INotifyDataErrorInfo members:

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        public bool HasErrors
        {
            get { return this.ValidationErrors.Any(); }
        }

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or null or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
        /// <returns>
        /// The validation errors for the property or entity.
        /// </returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !this.ValidationErrors.ContainsKey(propertyName)) return null;
            return (this.ValidationErrors.Keys.OfType<string>().Contains(propertyName)) ?
                this.ValidationErrors[propertyName] : Enumerable.Empty<string>();
        }
        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether the view model data valid.
        /// </summary>
        /// <value>
        /// <c>true</c> if the view model data valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsViewModelDataValid
        {
            get { return this._isViewModelDataValid; }
            set { base.SetProperty(ref this._isViewModelDataValid, value); }
        }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>The validation errors.</value>
        public Dictionary<string, List<string>> ValidationErrors
        {
            get { return this._validationErrors; }
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="error">The error.</param>
        /// <param name="isWarning">if set to <c>true</c> [is warning].</param>
        protected void AddError(object sender, string propertyName, string error, bool isWarning)
        {
            if (!this.ValidationErrors.ContainsKey(propertyName))
                this.ValidationErrors[propertyName] = new List<string>();

            if (!this.ValidationErrors[propertyName].Contains(error))
            {
                if (isWarning) this.ValidationErrors[propertyName].Add(error);
                else this.ValidationErrors[propertyName].Insert(0, error);
                this.RaiseDataErrorsChanged(sender, propertyName);
            }
        }

        /// <summary>
        /// Removes the error.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="error">The error.</param>
        protected void RemoveError(object sender, string propertyName, string error)
        {
            if (this.ValidationErrors.ContainsKey(propertyName) &&
                this.ValidationErrors[propertyName].Contains(error))
            {
                this.ValidationErrors[propertyName].Remove(error);
                if (this.ValidationErrors[propertyName].Count == 0)
                    this.ValidationErrors.Remove(propertyName);

                this.RaiseDataErrorsChanged(sender, propertyName);
            }
        }

        /// <summary>
        /// Supports the <see cref="MetadataType"/>.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        protected void SupportMetadataTypeAttribute(Type dataType)
        {
            var provider = new AssociatedMetadataTypeTypeDescriptionProvider(dataType);
            TypeDescriptor.AddProviderTransparent(provider, dataType);
        }

        /// <summary>
        /// Validates the instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        protected void ValidateInstance(object instance)
        {
            var results = new List<ValidationResult>();
            this.IsViewModelDataValid = Validator.TryValidateObject(instance, new ValidationContext(instance), results);

            results.ForEach(i =>
            {
                var propertyNames = (i.MemberNames.Count() > 1) ? string.Join(", ", i.MemberNames) : i.MemberNames.FirstOrDefault();
                this.AddError(instance, propertyNames, i.ErrorMessage, isWarning: false);
            });
        }

        /// <summary>
        /// Validates the property.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected void ValidateProperty(object instance, object propertyValue, [CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("The required Property Name to validate is expected.");

            var context = new ValidationContext(instance) { MemberName = propertyName };
            var results = new List<ValidationResult>();
            this.IsViewModelDataValid = Validator.TryValidateProperty(propertyValue, context, results);
            results.ForEach(i => this.AddError(instance, propertyName, i.ErrorMessage, isWarning: false));
        }

        void RaiseDataErrorsChanged(object sender, string propertyName)
        {
            if (ErrorsChanged != null) ErrorsChanged(sender, new DataErrorsChangedEventArgs(propertyName));
        }

        bool _isViewModelDataValid;

        Dictionary<string, List<string>> _validationErrors;
    }
}
