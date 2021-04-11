# TreeHelper

Namespace: **ES.Tools.UI**

Contains extension methods that help navigate through the WPF visual tree and logical tree.

### Methods

**GetParent&lt;T&gt;**

Returns the parent object of a certain type by using the logical or visual tree.

**GetLogicalParent&lt;T&gt;**

Returns the parent object of a certain type by using the WPF logical tree.

**GetVisualParent&lt;T&gt;**

Returns the parent object of a certain type by using the WPF visual tree.

**GetLogicalChild&lt;T&gt;**

Returns the first child object of a certain type by using the WPF logical tree.

**GetVisualChild&lt;T&gt;**

Returns the first child object of a certain type by using the WPF visual tree.

**GetLogicalChildren&lt;T&gt;**

Returns all child objects of a certain type by using the WPF logical tree.

**GetVisualChildren&lt;T&gt;**

Returns all child objects of a certain type by using the WPF visual tree.

**GetWindow**

Returns the parent window of a *DependencyObject*.

### Usage

In the example, we will try to find the parent *Button* of a rectangle.

```CSharp
FrameworkElement child = _window.yellowRectangle;
var parent = child.GetParent<Button>();
```
