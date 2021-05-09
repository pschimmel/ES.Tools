# TreeHelper

Namespace: **ES.Tools.UI**

Contains extension methods that help navigate through the WPF visual tree and logical tree.

### Methods

**GetParent&lt;T&gt;**(Func<T, bool> *[optional]*)

Returns the parent object of a certain type by using the logical or visual tree. Takes an optional delegate that can be used to search for an element with certain properties.

**GetLogicalParent&lt;T&gt;**(Func<T, bool> *[optional]*)

Returns the parent object of a certain type by using the WPF logical tree. Takes an optional delegate that can be used to search for an element with certain properties.

**GetVisualParent&lt;T&gt;**(Func<T, bool> *[optional]*)

Returns the parent object of a certain type by using the WPF visual tree. Takes an optional delegate that can be used to search for an element with certain properties.

**GetLogicalChild&lt;T&gt;**()

Returns the first child object of a certain type by using the WPF logical tree.

**GetVisualChild&lt;T&gt;**()

Returns the first child object of a certain type by using the WPF visual tree.

**GetLogicalChildren&lt;T&gt;**()

Returns all child objects of a certain type by using the WPF logical tree.

**GetVisualChildren&lt;T&gt;**()

Returns all child objects of a certain type by using the WPF visual tree.

**GetWindow**

Returns the parent window of a *DependencyObject*.

**IsChildOf(DependencyObject)**

Returns true, if the current *DependencyObject* is in the visual tree of the parent object.

### Usage

In the example, we will try to find the parent *Button* of a rectangle with the name "okButton".

```CSharp
FrameworkElement child = _window.yellowRectangle;
var parent = child.GetParent<Button>(x => x.Name == "okButton");
```