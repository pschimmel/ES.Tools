# Switch
Namespace: **ES.Tools.Controls**

The **Switch** is a complete restyled toggle button control. It supports all features of the WPF *ToggleButton*.

**Properties**

* *CornerRadius* - One of the *MeterType* enumeration values, Bar or Column.
* *OnContent* - Content that is displayed on the switch when the state is "On". Default value is the text "On".
* *OffContent* - Content that is displayed on the switch when the state is "Off". Default value is the text "Off".
* *SwitchWidth* - Width of the switch thumb.
* *SwitchBrush* - Brush of the switch thumb.
* *SwitchBorderBrush* - Brush of the switch thumb border.
* *SwitchBorderThickness* - Thickness of the switch thumb border.

**Usage**

``` XML
<controls:Switch Background="White"
                 BorderBrush="Black"
                 BorderThickness="2"
                 IsThreeState="True"
                 SwitchBorderBrush="Black"
                 SwitchBorderThickness="2"
                 SwitchBrush="White">
```
**Example**

![Switch example](Images/Switch.gif "Switch")
