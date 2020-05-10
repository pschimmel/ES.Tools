# ES.Tools
General useful WPF controls and helpers.

[![Build status](https://ci.appveyor.com/api/projects/status/jd0r84sejxmxysr3?svg=true)](https://ci.appveyor.com/project/pschimmel/es-tools)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/pschimmel/es-tools)](https://ci.appveyor.com/project/pschimmel/es-tool)
[![Nuget](https://img.shields.io/nuget/v/ES.Tools)](https://www.nuget.org/packages/ES.Tools/)
[![GitHub issues](https://img.shields.io/github/issues/pschimmel/es.tools)](https://github.com/pschimmel/ES.Tools/issues)

## Adorners
- **DataTemplateAdorner** - Adorner that will take a DataTemplate and a data object to render the adorner.
- **ControlAdorner** - Adorner that renders any control.
 
## Controls
- **AutoGrayableImage** - Image control that automatically turns itself into a grayscale image when disabled.
- **CustomItemsControl** - *ItemsControl* that uses a *ContentControl* as item container. This enables implicit *ViewModel* data templates.

## Converters
- **ColorToStringConverter** - Converts a WPF *Color* to a string and back.
- **TextTrimmingConverter** - Converts a string into a string with a limited amount of characters.

## Effects
- **GrayscaleEffect** - WPF *Effect* that turns the control into a monochrome colored control.

## Helpers
- **TreeHelperExtensions** - Extension methods that help navigate the WPF visual tree and logical tree.

## ES.MVVM
- **NotifyObject** - Basic abstract implementation of *INotifyPropertyChanged*.
- **ViewModel** - Basic abstract implementation of a *ViewModel*. Inherits from *NotifyObject* and implements *IDisposable*.
- **ActionCommand** - *ICommand* implementation, updating automatically when the CanExecute changes.