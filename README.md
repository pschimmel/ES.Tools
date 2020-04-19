# ES.WPF
General useful WPF controls and helpers.

## Adorner Controls
- **DataTemplateAdorner** - Adorner that will take a DataTemplate and a data object to render the adorner.
- **ControlAdorner** - Adorner that renders any control.

## Converters
- **ColorToStringConverter** - Converts a WPF Color to a string and back.
- **TextTrimmingConverter** - Converts a string into a string with a limited amount of characters.

## Helpers
- **TreeHelperExtensions** - Extension methods that help navigate the WPF visual tree and logical tree.

# ES.MVVM
Everything you need to do basic MVVM.

- **NotifyObject** - Basic abstract implementation of INotifyPropertyChanged.
- **ViewModel** - Basic abstract implementation of a ViewModel. Inherits from NotifyObject and implements IDisposable.