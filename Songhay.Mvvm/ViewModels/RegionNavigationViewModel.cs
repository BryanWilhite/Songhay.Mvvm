using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;
using System.Windows.Input;

namespace Songhay.Mvvm.ViewModels
{
    /// <summary>
    /// Defines MVVM-binding with Prism Region-based Navigation
    /// </summary>
    public class RegionNavigationViewModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegionNavigationViewModel"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="regionName">Name of the region.</param>
        /// <exception cref="System.NullReferenceException">The expected Prism Region Manager is not here.</exception>
        public RegionNavigationViewModel(IRegionManager regionManager, string regionName)
        {
            if (regionManager == null) throw new NullReferenceException("The expected Prism Region Manager is not here.");
            this._regionManager = regionManager;

            this._regionManager.Regions.CollectionChanged += (s, args) =>
            {
                var regionOne = this._regionManager.Regions.FirstOrDefault(i => i.Name == regionName);
                if (regionOne == null) return;
                if (this._regionNavigationService != null) return;

                this._regionNavigationService = this._regionManager.Regions.First(i => i.Name == regionName).NavigationService;
                this._regionNavigationService.Navigated += RegionOneNavigationServiceOnNavigated;
                this._regionNavigationService.Navigating += RegionOneNavigationServiceOnNavigating;
                this._regionNavigationService.NavigationFailed += RegionOneNavigationServiceOnNavigationFailed;

                this._regionNavigationJournal = this._regionNavigationService.Journal;
            };

            this.NavigationCommand = new DelegateCommand<string>(indicator =>
            {
                switch (indicator)
                {
                    case "forward":
                        this._regionNavigationJournal.GoForward();
                        break;
                    case "back":
                        this._regionNavigationJournal.GoBack();
                        break;
                    default:
                        var uriParts = indicator.Split(new [] {'?'});
                        if(uriParts.Count()==2)
                            this.NavigationParameters = new NavigationParameters(uriParts.Last());

                        if (this.NavigationResultAction == null)
                            this._regionManager.RequestNavigate(regionName, new Uri(indicator, UriKind.Relative), this.NavigationParameters);
                        else
                            this._regionManager.RequestNavigate(regionName, new Uri(indicator, UriKind.Relative), this.NavigationResultAction, this.NavigationParameters);
                        break;
                }
            });
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Prism Navigation Journal can go back.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoBack
        {
            get { return this._canGoBack; }
            set
            {
                this.SetProperty(ref this._canGoBack, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Prism Navigation Journal can go forward.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoForward
        {
            get { return this._canGoForward; }
            set
            {
                this.SetProperty(ref this._canGoForward, value);
            }
        }

        /// <summary>
        /// Gets the navigation command.
        /// </summary>
        /// <value>
        /// The navigation command.
        /// </value>
        public ICommand NavigationCommand { get; private set; }

        /// <summary>
        /// Gets or sets the navigation parameters.
        /// </summary>
        /// <value>
        /// The navigation parameters.
        /// </value>
        public NavigationParameters NavigationParameters { get; set; }

        /// <summary>
        /// Gets or sets the navigation result action.
        /// </summary>
        /// <value>
        /// The navigation result action.
        /// </value>
        public Action<NavigationResult> NavigationResultAction { get; set; }

        protected virtual void RegionOneNavigationServiceOnNavigated(object sender, RegionNavigationEventArgs e)
        {
            this.CanGoBack = e.NavigationContext.NavigationService.Journal.CanGoBack;
            this.CanGoForward = e.NavigationContext.NavigationService.Journal.CanGoForward;
        }

        protected virtual void RegionOneNavigationServiceOnNavigationFailed(object sender, RegionNavigationFailedEventArgs e)
        {
        }

        protected virtual void RegionOneNavigationServiceOnNavigating(object sender, RegionNavigationEventArgs e)
        {
        }

        IRegionManager _regionManager;
        IRegionNavigationJournal _regionNavigationJournal;
        IRegionNavigationService _regionNavigationService;

        bool _canGoBack;
        bool _canGoForward;
    }
}
