# ES.Tools

Great useful WPF controls and helpers.

[![Build status](https://ci.appveyor.com/api/projects/status/jd0r84sejxmxysr3?svg=true)](https://ci.appveyor.com/project/pschimmel/es-tools)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/pschimmel/es-tools)](https://ci.appveyor.com/project/pschimmel/es-tool)
[![Nuget ES.Tools](https://img.shields.io/nuget/v/ES.Tools?label=ES.Tools&logo=nuget)](https://www.nuget.org/packages/ES.Tools/)
[![Nuget ES.Tools.Core](https://img.shields.io/nuget/v/ES.Tools.Core?label=ES.Tools.Core&logo=nuget)](https://www.nuget.org/packages/ES.Tools.Core/)
[![GitHub issues](https://img.shields.io/github/issues/pschimmel/es.tools)](https://github.com/pschimmel/ES.Tools/issues)

## Examples

Here are some examples of controls contained in **ES.Tools**: Gauge, ProgressCircle, and Switch

![Gauge example](Documentation/Images/Gauge.gif "Gauge")
![ProgressCircle example](Documentation/Images/IntermediateProgressCircle.gif "ProgressCircle")
![Switch example](Documentation/Images/Switch.gif "Switch")

## Basic Tools (*ES.Tools.Core*)

### Infrastructure

- [**Services**](Documentation/Services.md) - Singleton class that globally manages service classes.
- [**TempServiceReplacement**](Documentation/TempServiceReplacement.md) - Utility class that temporarily replaces services, e.g. for unit testing.
- [**Utilities**](Documentation/Utilities.md) - Useful tools and extensions.

### MVVM

- [**ActionCommand**](Documentation/ActionCommand.md) - *ICommand* implementation, updating automatically when the *CanExecute* changes.
- [**NotifyObject**](Documentation/NotifyObject.md) - Basic abstract implementation of *INotifyPropertyChanged*.
- [**ViewFactory**](Documentation/ViewFactory.md) - Singleton class that manages the dependency between *ViewModel* and *View*.
- [**ViewModel**](Documentation/ViewModel.md) - Basic abstract implementation of a *ViewModel*. Inherits from *NotifyObject* and implements *IDisposable*.
- [**ViewModelExtensions**](Documentation/ViewModelExtensions.md) - Helpful extension methods that can be use in the context of *ViewModels*.

## Controls (*ES.Tools*)

### Adorners

- [**ControlAdorner**](Documentation/ControlAdorner.md) - Adorner that renders any control.
- [**DataTemplateAdorner**](Documentation/DataTemplateAdorner.md) - Adorner that will take a *DataTemplate* and a data object to render the adorner.

### Behaviors

- [**AutoScrollToCurrentItemBehavior**](Documentation/AutoScrollToCurrentItemBehavior.md) - Tries to bring the current item into the view. Works with any *Selector* control. 
- [**TextBoxBehavior**](Documentation/TextBoxBehavior.md) - Use the property *SelectAllTextOnFocus* to automatically select the whole text when the control is focused. 
- [**PasswordBoxBehavior**](Documentation/PasswordBoxBehavior.md) - Same as *TextBoxBehavior*, but for the WPF *PasswordBox* control.
 
### Controls

- [**Accordion**](Documentation/Accordion.md) - *Accordion* control that contains headered items that can be collapsed and expanded.
- [**AnimatedProgressBar**](Documentation/AnimatedProgressBar.md) - *ProgressBar* where the steps in between are animated.
- [**AutoGrayableImage**](Documentation/AutoGrayableImage.md) - *Image* control that automatically turns itself into a gray scale image when disabled.
- [**CustomItemsControl**](Documentation/CustomItemsControl.md) - *ItemsControl* that uses a *ContentControl* as item container. This enables implicit *ViewModel* data templates.
- [**Donut**](Documentation/Donut.md) - Simple control that looks like a donut. 
- [**DonutSegment**](Documentation/DonutSegment.md) - Simple control that looks like a segment of a donut. 
- [**Gauge**](Documentation/Gauge.md) - Control that looks like a real gauge with indicator, warning and error areas. 
- [**Indicator**](Documentation/Indicators.md) - Control that looks like a pin. 
- [**Meter**](Documentation/Meter.md) - Control that looks like a vertical or horizontal meter with an indicator. 
- [**OfficeSlider**](Documentation/OfficeSlider.md) - Restyling of the WPF slider that it looks more similar to the slider in Office applications.
- [**PieSegment**](Documentation/PieSegment.md) - Simple control that looks like a segment of a pie.
- [**ProgressCircle**](Documentation/ProgressCircle.md) - A simple circular progress bar.
- [**Switch**](Documentation/Switch.md) - A switch button control.

### Converters

- [**BooleanToVisibilityConverter**](Documentation/Converters.md) - Converts a boolean value to a *System.Windows.Visibility*.
- [**ColorToStringConverter**](Documentation/Converters.md) - Converts a WPF *System.Windows.Media.Color* to a string and back.
- [**DateTimeToTimeSpanConverter**](Documentation/Converters.md) - Converts a *System.DateTime* into a *System.TimeSpan*. This can be used for data binding when the model property has to be a *System.DateTime*.
- [**TextTrimmingConverter**](Documentation/Converters.md) - Converts a string into a string with a limited number of characters. If the text exceeds the number of characters "..." is added.

### Effects

- [**GrayscaleEffect**](Documentation/GrayscaleEffect) - WPF *Effect* that turns the control into a monochrome colored control.

### UI

- [**DependencyPropertyWatcher**](Documentation/DependencyPropertyWatcher.md) - Watches a dependency property and casts an event when the value changes.
- [**DispatcherWrapper**](Documentation/DispatcherWrapper.md) - Wrapper that wraps a WPF dispatcher. In unit tests you can easily replace the default dispatcher by a tesing implementation.
- [**TreeHelper**](Documentation/TreeHelper.md) - Extension methods that help navigate the WPF visual tree and logical tree.