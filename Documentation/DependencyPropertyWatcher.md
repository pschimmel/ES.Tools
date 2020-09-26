# DependencyPropertyWatcher
Namespace: **ES.Tools.UI**

The **DependencyPropertyWatcher** watches a dependency property and casts an event when the value changes.

Implements *IDisposable*.

**Usage**

In the example, the test object has a *DependencyProperty* of type *string*. Create a new **DependencyPropertyWatcher** with *string* as type argument and pass the object to be watched and the name of the property in the constructor.

A **PropertyChanged** event is cast whenever the property changes. You can obtain the changed value from the *Value* property of the **DependencyPropertyWatcher** object.

``` CSharp
var watcher = new DependencyPropertyWatcher<string>(testObject, nameof(TestDependencyObject.Text))
watcher.PropertyChanged += (s, e) => {
  // The new value can be obtained from the value property:
  string value = watcher.Value;
  // Do something
};
```