# AutoGrayableImage
Namespace: **ES.Tools.Controls**

Image control that automatically turns itself into a grayscale image when disabled.

This will turn any image into a grayscale version of the same image when the control gets disabled. It can be disabled by setting the *Disabled* property of the control itself or of any parent.

Usage:

``` XML
<controls:AutoGrayableImage Width="300" Height="200" Source="pack://application:,,,/ES.Tools.TestApp;Component/Images/Painting.jpg">
```

**Remark**
> Uses the [*GrayscaleEffect*](GrayscaleEffect.md) in **ES.Tools.Effects**.