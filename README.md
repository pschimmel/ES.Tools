# ES.Tools
General useful WPF controls and helpers.

[![Build status](https://ci.appveyor.com/api/projects/status/jd0r84sejxmxysr3?svg=true)](https://ci.appveyor.com/project/pschimmel/es-tools)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/pschimmel/es-tools)](https://ci.appveyor.com/project/pschimmel/es-tool)
[![Nuget](https://img.shields.io/nuget/v/ES.Tools)](https://www.nuget.org/packages/ES.Tools/)
[![GitHub issues](https://img.shields.io/github/issues/pschimmel/es.tools)](https://github.com/pschimmel/ES.Tools/issues)

## Adorners
- **ControlAdorner** - Adorner that renders any control.
- **DataTemplateAdorner** - Adorner that will take a DataTemplate and a data object to render the adorner.

## Base
- **DependencyPropertyHelper** - Watches a dependency property and casts an event when the value changes.
- **DispatcherWrapper** - Wrapper that wraps a WPF dispatcher. In unit tests you can easily replace the default dispatcher by a tesing implementation.
- **Tools** - Useful tools and extensions.

## Behaviors
- **AutoScrollToCurrentItemBehavior** - Tries to bring the current item into the view. Works with any *Selector* control. 
- **PasswordBoxBehavior** - Same as *TextBoxBehavior*, but for the WPF *PasswordBox* control.
- **TextBoxBehavior** - Use the property *SelectAllTextOnFocus* to automatically select the whole text when the control is focused. 
 
## Controls
- [**AutoGrayableImage**](Documentation/AutoGrayableImage.md) - Image control that automatically turns itself into a grayscale image when disabled.
- **CustomItemsControl** - *ItemsControl* that uses a *ContentControl* as item container. This enables implicit *ViewModel* data templates.

## Converters
- **BooleanToVisibilityConverter** - Converts a boolean value to a *System.Windows.Visibility*.
- **ColorToStringConverter** - Converts a WPF *System.Windows.Media.Color* to a string and back.
- **DateTimeToTimeSpanConverter** - Converts a *System.DateTime* into a *System.TimeSpan*. This can be used for data binding when the model property has to be a *System.DateTime*.
- **TextTrimmingConverter** - Converts a string into a string with a limited number of characters. If the text exceeds the number of characters "..." is added.

## Effects
- [**GrayscaleEffect**](Documentation/GrayscaleEffect.md) - WPF *Effect* that turns the control into a monochrome colored control.

## Helpers
- **TreeHelperExtensions** - Extension methods that help navigate the WPF visual tree and logical tree.

## MVVM
- **NotifyObject** - Basic abstract implementation of *INotifyPropertyChanged*.
- **ViewModel** - Basic abstract implementation of a *ViewModel*. Inherits from *NotifyObject* and implements *IDisposable*.
- **ActionCommand** - *ICommand* implementation, updating automatically when the CanExecute changes.