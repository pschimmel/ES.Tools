# Gauge
Namespace: **ES.Tools.Controls**

The **Gauge** control can be used to display values within a certain range.

**Properties**

* *Content* - Content of the content area, displayed in the lower part of the gauge.
* *ErrorValue* - Value where the error area begins. Set this to 0 to remove the area.
* *Header* - Content of the header area, displayed in the upper part of the gauge.
* *MaxValue* - Maximum value to display.
* *MinValue* - Minimum value to display.
* *SubTicks* - Number of sub tick lines between the main tick lines.
* *TotalTicks* - Number of total tick lines.
* *Value* - Current value.
* *WarningValue* - Value where the warning area begins. Set this to 0 to remove the area.

**Usage**

``` XML
<controls:Gauge BorderBrush="Black"
                BorderThickness="1"
                ErrorValue="90"
                MaxValue="100"
                MinValue="0"
                SubTicks="5"
                TotalTicks="20"
                WarningValue="70"
                Value="{Binding Value}">
  <controls:Gauge.Header>
    <TextBlock Foreground="Gray" Text="Pressure" />
  </controls:Gauge.Header>
  <controls:Gauge.Content>
    <TextBlock Foreground="Black" Text="bar" />
  </controls:Gauge.Content>
</controls:Gauge>
```
**Example**

![Gauge example](Images/Gauge.gif "Gauge")
