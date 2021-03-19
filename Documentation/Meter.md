# Meter
Namespace: **ES.Tools.Controls**

The **Meter** control can be used to display values within a certain range.

**Properties**

* *ErrorBrush* - Brush of the error area.
* *ErrorValue* - Value where the error area begins. Set this to 0 to remove the area.
* *MaxValue* - Maximum value to display.
* *MinValue* - Minimum value to display.
* *Orientation* - Horizontal or Vertical.
* *Type* - One of the *MeterType* enumeration values, Bar or Column.
* *Value* - Current value.
* *WarningBrush* - Brush of the warning area.
* *WarningValue* - Value where the warning area begins. Set this to 0 to remove the area.

**Usage**

``` XML
<controls:Meter Width="20"
                Height="150"                
                BorderBrush="Black"
                BorderThickness="1"
                ErrorValue="90"
                MaxValue="100"
                MinValue="0"
                Type="Column"
                WarningValue="70"
                Value="{Binding Value}" />

<controls:Meter Width="20"
                Height="150"
                BorderBrush="Black"
                BorderThickness="1"
                ErrorValue="90"
                MaxValue="100"
                MinValue="0"
                Type="Bar"
                WarningValue="70"
                Value="{Binding Value}" />
```
**Example**

![Meter example](Images/Meter.gif "Meter")
