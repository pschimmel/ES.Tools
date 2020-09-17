# ControlAdorner
Namespace: **ES.Tools.Adorners**

The **ControlAdorner** is an adorner that has the ability to render any WPF control.

**Usage**

To use the **ControlAdorner**, you have to provide the adorned element and the WPF control in the constructor.
Optionally, you can also give an *AdornerLayer*.

``` CSharp
var progressBar = new ProgressBar { Value = 20, Width = ShowControlAdornerButton.ActualWidth, Height = 10 };
var controlAdorner = new ControlAdorner(ShowControlAdornerButton, progressBar);
```