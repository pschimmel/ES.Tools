# ProgressCircle

Namespace: **ES.Tools.Controls**

![ProgressCircle example](Images/IntermediateProgressCircle.gif "ProgressCircle")
![ProgressCircle example 2](Images/ProgressCircle.png "ProgressCircle 2")

The **ProgressCircle** control is a circular progress control. It supports all features of the WPF *ProgressBar*.

### Properties

* *CircleWidth* - Width of the circle segment.

### Usage

``` XML
<controls:ProgressCircle CircleWidth="10" IsIndeterminate="true" />
<controls:ProgressCircle Background="LightGreen"
                         BorderBrush="{x:Null}"
                         BorderThickness="0"
                         CircleWidth="10"
                         Value="{Binding ProgressValue}" />
```
