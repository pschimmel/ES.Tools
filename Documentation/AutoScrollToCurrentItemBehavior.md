# AutoScrollToCurrentItemBehavior
Namespace: **ES.Tools.Behaviors**

The **AutoScrollToCurrentItemBehavior** is an extension for WPF *Selector* controls.

### AutoScrollToCurrentItem

Set the *AutoScrollToCurrentItem* attached dependency property to *true* to automatically scroll the current item into the view when it changes.

The behavior can be used with

* *DataGrid*s, 
* *ListBoxe*s, 
* *ListView*s, 

and any other WPF control derived from *Selector*.


**Usage**

``` XML
<ListBox ItemsSource="{Binding Items}"
         SelectedItem="{Binding SelectedItem}"
         behaviors:AutoScrollToCurrentItemBehavior.AutoScrollToCurrentItem="true" />
```