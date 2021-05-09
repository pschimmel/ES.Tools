# ProgressCircle

Namespace: **ES.Tools.Controls**

![Intermediate ProgressCircle example](Images/IntermediateProgressCircle.gif "Intermediate ProgressCircle")
![ProgressCircle example](Images/ProgressCircle.png "ProgressCircle")

The **ProgressCircle** control is a circular progress control. It supports all features of the WPF *ProgressBar* and more.

### Properties

* *CircleWidth* - Width of the circle segment.
* *IsIndeterminate* - Uses a continuous animation instead of the value to show the progress.
* *IsRotating* - Adds an additional rotation animation to the whole control.
* *Value* - The progress.

### Usage

``` XML
<controls:ProgressCircle CircleWidth="10" IsIndeterminate="true" />

<controls:ProgressCircle Background="LightGray"
                         BorderBrush="{x:Null}"
                         BorderThickness="0"
                         CircleWidth="10"
                         Foreground="Green"
                         IsRotating="true"
                         Value="{Binding ProgressValue}" />
```
