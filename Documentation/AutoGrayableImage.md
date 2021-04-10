# AutoGrayableImage

Namespace: **ES.Tools.Controls**

![AutoGrayableImage example: Enabled Image](Images/ImageColor.jpg "Enabled Image")
![AutoGrayableImage example: Disabled Image](Images/ImageGrayscale.jpg "Disabled Image")

The **AutoGrayableImage** control is an image control that automatically turns itself into a grayscale image when disabled.

This will turn any image into a grayscale version of the same image when the control gets disabled. It can be disabled by setting the *Disabled* property of the control itself or of any parent.

**AutoGrayableImage** is derived from the WPF *Image* control and therefore can be used in the same way. 

**Usage**

``` XML
<controls:AutoGrayableImage Width="300" Height="200" Source="pack://application:,,,/ES.Tools.TestApp;Component/Images/Painting.jpg">
```

**Remarks**
> Uses the [*GrayscaleEffect*](GrayscaleEffect) in **ES.Tools.Effects**.