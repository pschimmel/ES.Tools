# ViewFactory
Namespace: **ES.Tools.MVVM**

The **ViewFactory** manages the relationships between *ViewModel* and *View* by associating a type of a *View* to a specific type of a *ViewModel*.

### Usage

To use the **ViewFactory**, you have to register the *View* and *ViewModel* by calling *Register*. This is usually done in a central place, e.g. in *App.xaml.xs*. The *View* classes must implement the *IView* interface. The *ViewModel* classes must implement *IViewModel*. The included [*ViewModel*](ViewModel) base class already complies to that.

``` CSharp
ViewFactory.Instance.Register<SampleWindow, SampleViewModel>();
```

Then you can create a new instance of the *View* type by calling the *CreatePage* method of the *ViewFactory*.
In this example we already have an instance of the *ViewModel*: 

``` CSharp
var vm = new SampleViewModel();
var view = ViewFactory.Instance.CreatePage(vm);
view.ShowDialog();
```

*CreatePage* also has a generic overload that also creates the ViewModel for you. Optionally you can call *CreatePage* with arguments if your *ViewModel* has no parameterless constructor.  

``` CSharp
var view = ViewFactory.Instance.CreatePage<SampleViewModel>();
view.ShowDialog();
```

You have access to the *ViewModel* through the *DataContext* of the *View*.

### Remarks
>See also [*ViewModel*](ViewModel).