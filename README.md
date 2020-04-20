# ES.Tools
General useful WPF controls and helpers.

[![Build status](https://ci.appveyor.com/api/projects/status/jd0r84sejxmxysr3?svg=true)](https://ci.appveyor.com/project/pschimmel/es-tools)
![AppVeyor tests](https://img.shields.io/appveyor/tests/pschimmel/es-tools)
![Nuget](https://img.shields.io/nuget/v/ES.Tools)
![GitHub issues](https://img.shields.io/github/issues/pschimmel/es.tools)

## Adorner Controls
- **DataTemplateAdorner** - Adorner that will take a DataTemplate and a data object to render the adorner.
- **ControlAdorner** - Adorner that renders any control.

## Converters
- **ColorToStringConverter** - Converts a WPF Color to a string and back.
- **TextTrimmingConverter** - Converts a string into a string with a limited amount of characters.

## Helpers
- **TreeHelperExtensions** - Extension methods that help navigate the WPF visual tree and logical tree.

## ES.MVVM
- **NotifyObject** - Basic abstract implementation of INotifyPropertyChanged.
- **ViewModel** - Basic abstract implementation of a ViewModel. Inherits from NotifyObject and implements IDisposable.
