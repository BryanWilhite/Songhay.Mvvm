using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
#if SILVERLIGHT
using System.Windows;
#endif

namespace Songhay
{
    /// <summary>
    /// Utilities for the Framework <see cref="System.Windows.Threading.Dispatcher"/>
    /// and/or the <see cref="System.Windows.Threading.DispatcherObject.Dispatcher"/>.
    /// </summary>
    public static class FrameworkDispatcherUtility
    {
        /// <summary>
        /// Returns <c>true</c> when <see cref="System.Windows.Application.Current"/>
        /// is not null.
        /// </summary>
        public static bool HasCurrentWindowsApplication()
        {
            var windowsApplication = System.Windows.Application.Current;
            return (windowsApplication != null);
        }

        /// <summary>
        /// Determines whether there is a current <see cref="System.Threading.SynchronizationContext"/>
        /// for the Application Domain.
        /// </summary>
        /// <returns>Returns <c>true</c> when <c>SynchronizationContext.Current</c> is not null.</returns>
        /// <remarks>
        /// “…if you obtain your SynchronizationContext class via the static SynchronizationContext.Current method,
        /// it will return null if the thread is not a UI thread.”
        /// [http://daveonsoftware.blogspot.com/2007/11/dispatcher-versus-synchronizationcontex.html]
        /// </remarks>
        public static bool HasCurrentSynchronizationContext()
        {
            return (SynchronizationContext.Current != null);
        }

        /// <summary>
        /// Initialize the Framework Dispatcher.
        /// </summary>
        /// <remarks>
        /// The <see cref="System.Windows.Threading.Dispatcher"/> is not necessarily
        /// the same as the <see cref="System.Windows.Threading.DispatcherObject.Dispatcher"/>.
        /// </remarks>
        public static void Initialize()
        {
#if SILVERLIGHT
            windowsDispatcher = Deployment.Current.Dispatcher;
#else
            frameworkDispatcher = Dispatcher.CurrentDispatcher;
#endif
        }

        /// <summary>
        /// Initialize the Framework Dispatcher for a Windows Application.
        /// </summary>
        /// <remarks>
        /// The <see cref="System.Windows.Threading.Dispatcher"/> is not necessarily
        /// the same as the <see cref="System.Windows.Threading.DispatcherObject.Dispatcher"/>.
        /// </remarks>
        public static void InitializeForWindowsApplication()
        {
            if (!HasCurrentWindowsApplication()) throw new NullReferenceException("The expected Windows Application is not here.");

            frameworkDispatcher = System.Windows.Application.Current.Dispatcher;
        }

        /// <summary>
        /// Determines whether the method caller is on the same thread
        /// as the <see cref="System.Windows.Threading.Dispatcher"/>.
        /// </summary>
        /// <remarks>
        /// The “UI thread” is on the same thread as the <see cref="System.Windows.Threading.Dispatcher"/>.
        /// </remarks>
        public static bool IsCallingFromDispatcherThread()
        {
            return frameworkDispatcher.CheckAccess();
        }

        /// <summary>
        /// Invokes the specified <see cref="System.Action" />
        /// on the <see cref="System.Windows.Threading.Dispatcher" /> thread
        /// or enqueues for asynchronous invocation.
        /// </summary>
        /// <param name="action">The specified action.</param>
        public static void InvokeImmediatelyOrEnqueueForDispatcher(Action action)
        {
            InvokeImmediatelyOrEnqueueForDispatcher(action, DispatcherPriority.Normal);
        }

        /// <summary>
        /// Invokes the specified <see cref="System.Action" />
        /// on the <see cref="System.Windows.Threading.Dispatcher" /> thread
        /// or enqueues for asynchronous invocation.
        /// </summary>
        /// <param name="action">The specified action.</param>
        /// <param name="priority">The priority.</param>
        public static void InvokeImmediatelyOrEnqueueForDispatcher(Action action, DispatcherPriority priority)
        {
            if (action == null) throw new NullReferenceException("The expected action to invoke is not here.");

            if (!HasCurrentWindowsApplication()) action();
            else if (IsCallingFromDispatcherThread()) action();
            else frameworkDispatcher.BeginInvoke(action, priority);
        }

        /// <summary>
        /// Asynchronously invokes the specified <see cref="System.Action" />
        /// or enqueues for invocation by the <see cref="System.Windows.Threading.Dispatcher" />.
        /// </summary>
        /// <param name="action">The specified action.</param>
        public static Task InvokeOrEnqueueForDispatcherAsync(Action action)
        {
            return InvokeOrEnqueueForDispatcherAsync(action, DispatcherPriority.Normal);
        }

        /// <summary>
        /// Asynchronously invokes the specified <see cref="System.Action" />
        /// or enqueues for invocation by the <see cref="System.Windows.Threading.Dispatcher" />.
        /// </summary>
        /// <param name="action">The specified action.</param>
        /// <param name="priority">The priority.</param>
        public static Task InvokeOrEnqueueForDispatcherAsync(Action action, DispatcherPriority priority)
        {
            if (action == null) throw new NullReferenceException("The expected action to invoke is not here.");

            if (!HasCurrentWindowsApplication()) return Task.Factory.StartNew(action);
            else if (IsCallingFromDispatcherThread()) return Task.Factory.StartNew(action);
            else return frameworkDispatcher.BeginInvoke(action, priority).Task;
        }

        /// <summary>
        /// Sets the <see cref="System.Threading.SynchronizationContext"/>
        /// </summary>
        /// <remarks>
        /// This may be useful for certain unit-testing scenarios.
        /// “You should in general leave it up to the specific UI class library to set this correctly.
        /// Winforms automatically installs a WindowsFormsSynchronizationContext instance,
        /// WPF installs a DispatcherSynchronizationContext, ASP.NET installs a AspNetSynchronizationContext,
        /// a Store app installs WinRTSynchronizationContext, etcetera.
        /// Highly specific synchronization providers that are tuned to the way the UI thread dispatches events.”
        /// —Hans Passant
        /// [http://stackoverflow.com/questions/4658819/when-to-call-synchronizationcontext-setsynchronizationcontext-in-a-ui-applicat]
        /// </remarks>
        public static void SetSynchronizationContext()
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }

        /// <summary>
        /// Sets a “global” exception handler for the current
        /// <see cref="System.Windows.Threading.Dispatcher"/>.
        /// </summary>
        /// <param name="action">The action to take in response to the Exception event.</param>
        /// <remarks>
        /// This member is based on “WPF/Silverlight Exception Global Exception handling”
        /// by Digital Transformation LLC, Louisville KY
        /// [http://blog.ditran.net/wpfsilverlight-exception-global-exception-handling/]
        /// </remarks>
        public static void SetExceptionHandlerForFrameworkDispatcher(Action<object, DispatcherUnhandledExceptionEventArgs> action)
        {
            if (frameworkDispatcher == null) throw new NullReferenceException("The expected dispatcher is not here.");
            frameworkDispatcher.UnhandledException += (s, args) => action.Invoke(s, args);
        }

        /// <summary>
        /// Sets a “global” exception handler for the current
        /// <see cref="System.Windows.Threading.Dispatcher"/>
        /// (usually on the “UI thread”).
        /// </summary>
        /// <param name="action">The action to take in response to the Exception event.</param>
        /// <remarks>
        /// This member is based on “WPF/Silverlight Exception Global Exception handling”
        /// by Digital Transformation LLC, Louisville KY
        /// [http://blog.ditran.net/wpfsilverlight-exception-global-exception-handling/]
        /// </remarks>
        public static void SetExceptionHandler(Action<object, DispatcherUnhandledExceptionEventArgs> action)
        {
            if (!HasCurrentWindowsApplication()) throw new NullReferenceException("The expected Windows Application is not here.");
            System.Windows.Application.Current.DispatcherUnhandledException += (s, args) => action.Invoke(s, args);
        }

        /// <summary>
        /// Sets a “global” exception handler for the current
        /// <see cref="System.AppDomain"/>.
        /// </summary>
        /// <param name="action">The action to take in response to the Exception event.</param>
        /// <remarks>
        /// This member is based on “WPF/Silverlight Exception Global Exception handling”
        /// by Digital Transformation LLC, Louisville KY
        /// [http://blog.ditran.net/wpfsilverlight-exception-global-exception-handling/]
        /// </remarks>
        public static void SetExceptionHandler(Action<object, UnhandledExceptionEventArgs> action)
        {
            if (AppDomain.CurrentDomain == null) throw new NullReferenceException("The expected app domain is not here.");
            AppDomain.CurrentDomain.UnhandledException += (s, args) => action.Invoke(s, args);
        }

        static Dispatcher frameworkDispatcher;
    }
}
