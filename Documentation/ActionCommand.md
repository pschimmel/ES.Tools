# ActionCommand
Namespace: **ES.Tools.Core.MVVM** *(ES.Tools.Core)*

## ActionCommand

The **ActionCommand** can be used as ICommand implementation for defining commands in the *ViewModel*.

### Usage

In the example *ViewModel* we define a public readonly property of type *ICommand*. In the constructor, a new instance of an **ActionCommand** is assigned to that property.

The method *EditItemCommandExecute* will be executed when the command is executed.

*EditItemCommandCanExecute* returns a boolean defining if the command can currently be executed. This is optional. 

*RaiseCanExecuteChanged* triggers reevaluation of *EditItemCommandCanExecute*. 

```CSharp
public class MainViewModel : ViewModel
{
  private IItem _selectedItem;

  public VehicleViewModel()
  {
    EditItemCommand = new ActionCommand(EditItemCommandExecute, EditItemCommandCanExecute);
  }

  public IItem SelectedItem
  {
    get => _selectedItem;
    set
    {
      _selectedItem = value;
      EditItemCommand.RaiseCanExecuteChanged();
    }
  }

  public ICommand EditItemCommand { get; }

  private void EditItemCommandExecute()
  {
    var vm = new EditItemViewModel(SelectedItem);
    var view = ViewFactory.Instance.CreatePage(vm);
    view.ShowDialog();
  }

  private bool EditItemCommandCanExecute()
  {
    return SelectedItem != null;
  }
}
```

Using the command in XAML is rather easy. Just use DataBinding to bind the Command property of the control to the *ViewModel*.

``` XML
<Button Command="{Binding EditItemCommand}">Edit Item</Button/>
```

## ActionCommand&lt;T&gt;

The **ActionCommand&lt;T&gt;** is an **ActionCommand** that takes an item of type *T* as argument for the *Execute* and *CanExecute* methods.

###Usage

In the example *ViewModel* we define a public readonly property of type *ICommand*. In the constructor, a new instance of an **ActionCommand&lt;T&gt;** is assigned to that property.

The method *EditItemCommandExecute* will be executed when the command is executed. An item is passed as parameter.

*EditItemCommandCanExecute* returns a boolean defining if the command can currently be executed. This is optional. An item is passed as parameter.

*RaiseCanExecuteChanged* triggers reevaluation of *EditItemCommandCanExecute*. 

```CSharp
public class MainViewModel : ViewModel
{
  public VehicleViewModel()
  {
    Items = new ObservableCollection<IItem>();
    DeleteItemCommand = new ActionCommand<IItem>(DeleteItemCommandExecute, DeleteItemCommandCanExecute);
  }

  public ObservableCollection<IItem> Items { get; }

  public ICommand DeleteItemCommand { get; }

  private void DeleteItemCommandExecute(IItem item)
  {
    Items.Remove(item);
  }

  private bool DeleteItemCommandCanExecute(IItem item)
  {
    return item != null;
  }
}
```

Using the command in XAML is rather easy. Just use DataBinding to bind the Command property of the control to the *ViewModel*. In the example we're binding the current item of the *ListBox* control to the *CommandParameter* of the *Button*.

``` XML
<ListBox ItemsSource="{Binding Items}">
  <ListBox.ItemTemplate>
    <DataTemplate>
      <Grid>
        <Grid.ColumnDefinitions>
          <ColumnDefinition/>
          <ColumnDefinition Width="Auto"/>
        </Grid.ColumnDefinitions>
        <TextBlock Text="{Binding Name}" Margin="2"/>
        <Button Command="{Binding DeleteItemCommand}" CommandParameter="{Binding}" Padding="3,1">Remove</Button>
      </Grid>
    </DataTemplate>
  </ListBox.ItemTemplate>
</ListBox>
```