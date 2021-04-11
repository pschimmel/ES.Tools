# Converters
Namespace: **ES.Tools.Converters**

## BooleanToVisibilityConverter

Converts a boolean value to a *System.Windows.Visibility*.

## ColorToStringConverter

Converts a WPF *System.Windows.Media.Color* to a string and back. This can be used for example to bind a color to a *Model* or *ViewModel* property that can only contain string values. e.g. for saving in files or databases.

## DateTimeToTimeSpanConverter

Converts a *System.DateTime* into a *System.TimeSpan*. This can be used for data binding when the model property has to be a *System.DateTime*.

## TextTrimmingConverter

Converts a string into a string with a limited number of characters. If the text exceeds the number of characters "..." is added.
