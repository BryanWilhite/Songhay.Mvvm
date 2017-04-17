using Prism.Commands;
using Prism.Mvvm;
using Songhay.Models;
using System.Reflection;
using System.Windows.Input;

namespace Songhay.Mvvm.ViewModels
{
    /// <summary>
    /// About… View Model
    /// </summary>
    public class AboutViewModel : BindableBase, IFrameworkAssemblyInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        public AboutViewModel()
            : this(Assembly.GetExecutingAssembly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        /// <param name="dll">The DLL.</param>
        public AboutViewModel(Assembly dll)
        {
            this.SetProperties(dll);

            this.AboutCommand = new DelegateCommand(() =>
            {
                var location = "http://songhaysystem.com/"; //TODO: add About… (documentation) support for songhaysystem.com
                FrameworkUtility.StartProcess(location, string.Empty, false);
            });
        }

        /// <summary>
        /// Gets the about command.
        /// </summary>
        /// <value>
        /// The about command.
        /// </value>
        public ICommand AboutCommand { get; private set; }

        #region IFrameworkAssemblyInfo Members

        /// <summary>
        /// Gets or sets the assembly company.
        /// </summary>
        /// <value>The assembly company.</value>
        public string AssemblyCompany
        {
            get { return this._assemblyCompany; }
            set { this.SetProperty(ref this._assemblyCompany, value); }
        }

        /// <summary>
        /// Gets or sets the assembly copyright.
        /// </summary>
        /// <value>The assembly copyright.</value>
        public string AssemblyCopyright
        {
            get { return this._assemblyCopyright; }
            set { this.SetProperty(ref this._assemblyCopyright, value); }
        }

        /// <summary>
        /// Gets or sets the assembly description.
        /// </summary>
        /// <value>The assembly description.</value>
        public string AssemblyDescription
        {
            get { return this._assemblyDescription; }
            set { this.SetProperty(ref this._assemblyDescription, value); }
        }

        /// <summary>
        /// Gets or sets the assembly product.
        /// </summary>
        /// <value>The assembly product.</value>
        public string AssemblyProduct
        {
            get { return this._assemblyProduct; }
            set { this.SetProperty(ref this._assemblyProduct, value); }
        }

        /// <summary>
        /// Gets or sets the assembly title.
        /// </summary>
        /// <value>The assembly title.</value>
        public string AssemblyTitle
        {
            get { return this._assemblyTitle; }
            set { this.SetProperty(ref this._assemblyTitle, value); }
        }

        /// <summary>
        /// Gets or sets the assembly version.
        /// </summary>
        /// <value>The assembly version.</value>
        public string AssemblyVersion
        {
            get { return this._assemblyVersion; }
            set { this.SetProperty(ref this._assemblyVersion, value); }
        }

        /// <summary>
        /// Gets or sets the assembly version detail.
        /// </summary>
        /// <value>The assembly version detail.</value>
        public string AssemblyVersionDetail
        {
            get { return this._assemblyVersionDetail; }
            set { this.SetProperty(ref this._assemblyVersionDetail, value); }
        }

        #endregion

        void SetProperties(Assembly dll)
        {
            var data = new FrameworkAssemblyInfo(dll);
            FrameworkTypeUtility.SetProperties<FrameworkAssemblyInfo, AboutViewModel>(data, this);
        }

        string _assemblyCompany;
        string _assemblyCopyright;
        string _assemblyDescription;
        string _assemblyProduct;
        string _assemblyTitle;
        string _assemblyVersion;
        string _assemblyVersionDetail;
    }
}
