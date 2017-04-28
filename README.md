# Songhay.Mvvm

Shared Model, View, View-Model definitions for [Prism 6.0](https://github.com/PrismLibrary) over [Unity](https://github.com/unitycontainer/unity), published as [a NuGet package](https://www.nuget.org/packages/Songhay.Mvvm/), supporting `net4.61` (so you can share cross-platform libraries under .NET Standard 1.4)…

**NuGet package:** [`Songhay.Mvvm`](https://www.nuget.org/packages/Songhay.Mvvm/)

## Extensions

This package contains [extension methods](https://github.com/BryanWilhite/Songhay.Mvvm/tree/master/Songhay.Mvvm/Extensions) for Prism definitions, including `IModule` and `BindableBase`.

## Fonts

This package contains the [Segoe UI](https://www.microsoft.com/typography/fonts/family.aspx?FID=331) font from Microsoft as a Resource. (I have not reviewed the Microsoft typography [Font Redistribution FAQ](https://www.microsoft.com/typography/RedistributionFAQ.mspx).)

## Models

This package defines models for conventional Prism Events, etc. There’s a [conventional `EventPayload` class](https://github.com/BryanWilhite/Songhay.Mvvm/blob/master/Songhay.Mvvm/Models/EventPayload.cs) with a `Tag` property that can be used to filter/route in subscriber handlers.

## Resources

The XAML resource dictionaries embedded in this package are basically for my company [Songhay System](http://songhaysystem.com) and should be ignored.

## Value Converters

This package contains [a collection of XAML value converters](https://github.com/BryanWilhite/Songhay.Mvvm/tree/master/Songhay.Mvvm/ValueConverters) that emerged out of my needs as a consultant for almost two decades.

## View Models

The [reusable View models](https://github.com/BryanWilhite/Songhay.Mvvm/tree/master/Songhay.Mvvm/ViewModels) in this package include `BindableBaseWithValidation` and `RegionNavigationViewModel`.

## Utilities

There are some WPF App utilities, such as [`FrameworkDispatcherUtility`](https://github.com/BryanWilhite/Songhay.Mvvm/blob/master/Songhay.Mvvm/FrameworkDispatcherUtility.cs) that defines how to check the current thread and allow components to be unit-tested outside of WPF.