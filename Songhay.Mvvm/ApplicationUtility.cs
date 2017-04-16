using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Songhay
{
    using Extensions;
    using Globalization;
    using Models;

    /// <summary>
    /// Static members for WPF Applications
    /// </summary>
    public static partial class ApplicationUtility
    {
        /// <summary>
        /// Gets the Resource display text.
        /// </summary>
        /// <param name="text">The text.</param>
        public static string GetResourceDisplayText(string text)
        {
            return GetResourceDisplayText(text, null);
        }

        /// <summary>
        /// Gets the resource display text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="maximumTextLength">Maximum length of the text.</param>
        public static string GetResourceDisplayText(string text, int? maximumTextLength)
        {
            text = Uri.UnescapeDataString(text)
                .Split('/').Last()
                .Replace(".xaml", string.Empty)
                .Replace("+", " ")
                .Replace("-", ": ");
            text = TextInfoUtility.ToTitleCase(text);

            if (maximumTextLength.HasValue && !string.IsNullOrEmpty(text) && (text.Length > maximumTextLength))
                text = string.Concat(text.Substring(0, maximumTextLength.GetValueOrDefault() - 1), "…");

            return text;
        }

        /// <summary>
        /// Hides the <see cref="Window"/> on closing.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="args">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        public static void HideWindowOnClosing(this Window window, CancelEventArgs args)
        {
            if (!FrameworkDispatcherUtility.HasCurrentWindowsApplication()) return;
            if (window == null) return;
            if (args == null) return;

            args.Cancel = true;

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                (DispatcherOperationCallback)(i =>
                {
                    window.Hide();
                    return null;
                }), null);
        }

        /// <summary>
        /// Determines whether Application is in design mode.
        /// </summary>
        public static bool IsInDesignMode()
        {
            if (isInDesignMode.HasValue) return isInDesignMode.GetValueOrDefault();
            var dependencyProperty = DesignerProperties.IsInDesignModeProperty;
            var descriptor = DependencyPropertyDescriptor.FromProperty(dependencyProperty, typeof(FrameworkElement));
            isInDesignMode = (bool)descriptor.Metadata.DefaultValue;
            return isInDesignMode.GetValueOrDefault();
        }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>A <see cref="System.String"/> representation of the Resource.</returns>
        public static string LoadResource(Uri uri)
        {
            var appStream = Application.GetResourceStream(uri);
            using (var reader = new StreamReader(appStream.Stream))
            {
                var s = reader.ReadToEnd();
                return s;
            }
        }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        /// <typeparam name="TControl">The type of the control.</typeparam>
        /// <param name="uri">The URI.</param>
        public static TControl LoadResource<TControl>(Uri uri) where TControl : UIElement
        {
            var xaml = LoadResource(uri);
            return LoadResource<TControl>(xaml);
        }

        /// <summary>
        /// Loads the resource.
        /// </summary>
        /// <typeparam name="TControl">The type of the control.</typeparam>
        /// <param name="xaml">The xaml.</param>
        public static TControl LoadResource<TControl>(string xaml) where TControl : UIElement
        {
            var control = (TControl)XamlReader.Parse(xaml);
            return control;
        }

        /// <summary>
        /// Lists the packed resources.
        /// </summary>
        /// <param name="dll">The DLL.</param>
        public static IEnumerable<DisplayItemModel> ListPackedResources(Assembly dll)
        {
            return ListPackedResources(dll, null);
        }

        /// <summary>
        /// Lists the packed resources.
        /// </summary>
        /// <param name="dll">The DLL.</param>
        /// <param name="maximumDisplayTextLength">Maximum length of the display text.</param>
        /// <returns></returns>
        public static IEnumerable<DisplayItemModel> ListPackedResources(Assembly dll, int? maximumDisplayTextLength)
        {
            if (dll == null) return Enumerable.Empty<DisplayItemModel>();

            var list = new List<DisplayItemModel>();
            var simpleAsmName = new AssemblyName(dll.FullName).Name;

            var data = ListResources(dll);
            data.ForEachInEnumerable(i =>
            {
                var key = i.Replace(string.Format("/{0};Component/", simpleAsmName), string.Empty);
                if (key.StartsWith("packedxaml/") || key.StartsWith("packedxml/"))
                {
                    var resource =
                        new DisplayItemModel
                        {
                            DisplayText = ApplicationUtility.GetResourceDisplayText(key, maximumDisplayTextLength),
                            ItemName = key,
                            ResourceIndicator = new Uri(i, UriKind.Relative)
                        };
                    list.Add(resource);
                }
            });

            return list.OrderBy(i => i.ItemName);
        }

        /// <summary>
        /// Lists the resources.
        /// </summary>
        /// <param name="assemblyType">Type of the assembly.</param>
        public static IEnumerable<string> ListResources(Type assemblyType)
        {
            var dll = Assembly.GetAssembly(assemblyType);
            return ListResources(dll);
        }

        /// <summary>
        /// Lists the resources.
        /// </summary>
        /// <param name="dll">The DLL.</param>
        public static IEnumerable<string> ListResources(Assembly dll)
        {
            if (dll == null) return Enumerable.Empty<string>();

            var simpleAsmName = new AssemblyName(dll.FullName).Name;
            var resourceName = string.Concat(simpleAsmName, ".g.resources");

            Func<Stream> getStream = () =>
            {
                return dll.GetManifestResourceStream(resourceName);
            };

            var list = new List<string>();
            using (var reader = new ResourceReader(getStream()))
            {
                var sb = new StringBuilder();
                foreach (DictionaryEntry entry in reader)
                {
                    var key = (string)entry.Key;
                    key = sb.AppendFormat("/{0};Component/{1}", simpleAsmName, key).ToString();
                    list.Add(key);
                    sb.Clear();
                }
            }

            return list;
        }

        static bool? isInDesignMode;
    }
}
