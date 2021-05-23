# ViewFactory
Namespace: **ES.Tools.Core.MVVM** *(ES.Tools.Core)*

The **ViewFactory** is a singleton factory class that manages the relationships between *ViewModel* and *View* by associating a type of a *View* to a specific type of a *ViewModel*.

### Methods

**Register&lt;TView, ModelTView&gt;()**

Register a new View/ViewModel relationship.

**Type[] RegisteredViewModelTypes()**

Returns an array of the registered ViewModel types..

**Type GetViewType(Type viewModelType)**

 Returns the View type for a ViewModel type.

**IView CreateView&lt;T&gt;(params object[] args)**

Creates a new matching View with a new instance of the ViewModel.
Optionally takes parameters that are used as parameters for the ViewModels constructor.

**IView CreateView(IViewModel viewModel, bool setOwner [optional])**

Creates a new matching View for the given ViewModel.

### Usage

To use the **ViewFactory**, you have to register the *View* and *ViewModel* by calling *Register*. This is usually done in a central place, e.g. in *App.xaml.xs*. The *View* classes must implement the *IView* interface. The *ViewModel* classes must implement *IViewModel*. The included [*ViewModel*](ViewModel) base class already complies to that.

``` CSharp
ViewFactory.Instance.Register<SampleWindow, SampleViewModel>();
```

Then you can create a new instance of the *View* type by calling the *CreateView* method of the *ViewFactory*.
In this example we already have an instance of the *ViewModel*: 

``` CSharp
var vm = new SampleViewModel();
var view = ViewFactory.Instance.CreateView(vm);
view.ShowDialog();
```

*CreateView* also has a generic overload that also creates the ViewModel for you. Optionally you can call *CreateView* with arguments if your *ViewModel* has no parameterless constructor.  

``` CSharp
var view = ViewFactory.Instance.CreateView<SampleViewModel>();
view.ShowDialog();
```

You have access to the *ViewModel* through the *DataContext* of the *View*.

### Remarks
>See also [*ViewModel*](ViewModel.md).