# DataTemplateAdorner
Namespace: **ES.Tools.Adorners**

The **DataTemplateAdorner** is an adorner that will take a *DataTemplate* and a data object to render the adorner.

### Usage

For the example, we first need a simple *ViewModel* and a *DataTemplate* defined in one of the resource dictionaries:

``` CSharp
public class DataTemplateAdornerViewModel
{
  public string Text { get; set; }
}
```

``` XML
<DataTemplate x:Key="YellowCircleText">
  <Grid>
    <Ellipse Fill="LightYellow" Stroke="Black" StrokeThickness="1" />
    <TextBlock Margin="10" Text="{Binding Text}" />
  </Grid>
</DataTemplate>
```

To use the **DataTemplateAdorner**, you have to provide a content in form of a *ViewModel*, the *DataTemplate* and the adorned element in the constructor.
Optionally, you can also give an *AdornerLayer*.

``` CSharp
var vm = new DataTemplateAdornerViewModel { Text = "Test" };
var template = (DataTemplate)TryFindResource("YellowCircleText");
var dataTemplateAdorner = new DataTemplateAdorner(ShowDataTemplateAdornerButton, vm, template);
```