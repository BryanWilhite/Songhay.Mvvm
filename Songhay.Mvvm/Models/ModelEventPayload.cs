using System;

namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Defines Prism “Event payload” for Model CRUD operations.
    /// </summary>
    /// <typeparam name="TModel">The type of the entity model.</typeparam>
    public class ModelEventPayload<TModel> : EditorEventPayload
    {
        /// <summary>
        /// Gets or sets the name of the changed property.
        /// </summary>
        /// <value>
        /// The name of the changed property.
        /// </value>
        public string ChangedPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public TModel Model { get; set; }

        public override string ToString()
        {
            var s = string.Format("IsDelete: {0}, IsEdit: {1}, IsInsert: {2}, IsUpdate: {3}", this.IsDelete, this.IsEdit, this.IsInsert, this.IsUpdate);
            if (!string.IsNullOrEmpty(this.ChangedPropertyName)) s = string.Format("ChangedPropertyName: {0}, {1}", this.ChangedPropertyName, s);
            if(this.Model != null) s = string.Format("{0}{1}Model: {2}", s, Environment.NewLine, this.Model.ToString());
            return s;
        }
    }
}
