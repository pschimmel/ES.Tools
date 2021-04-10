# OfficeSlider

![OfficeSlider example](Images/OfficeSlider.png "Office Slider")

The **OfficeSliderStyle** is a style that can be applied to WPF *Slider* controls to change their appearance to be more office like.

**Usage**

Include the resource dictionary containing the slider style in your windows resources.

``` XML
<Window.Resources>
  <ResourceDictionary>
    <ResourceDictionary.MergedDictionaries>
      <ResourceDictionary Source="pack://application:,,,/ES.Tools;component/Themes/OfficeSlider.xaml" />
    </ResourceDictionary.MergedDictionaries>
  </ResourceDictionary>
</Window.Resources>
```

Assign the style key to the *Slider* control.

``` XML
<Slider Style="{DynamicResource OfficeSliderStyle}"/>
```