
namespace Songhay.Mvvm.Models
{
    /// <summary>
    /// Pairs a VM and its source Model
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <typeparam name="TSourceModel">The type of the source model.</typeparam>
    public class ViewModelSourceModelPair<TViewModel, TSourceModel>
    {
        /// <summary>
        /// Gets or sets the source model.
        /// </summary>
        /// <value>
        /// The source model.
        /// </value>
        public TSourceModel SourceModel { get; set; }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public TViewModel ViewModel { get; set; }
    }
}
