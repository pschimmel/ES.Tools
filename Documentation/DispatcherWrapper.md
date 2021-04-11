# DispatcherWrapper
Namespace: **ES.Tools.UI**

Wraps a dispatcher to execute actions on the UI thread from any place in your application. Can be used to inject a mocked dispatcher implementation in unit tests. Implements interface *IDispatcherWrapper*.

Before an action is executed on the UI thread, the **DispatcherWrapper** checks if the action can run on the current thread instead.

### Methods

**BeginInvokeIfRequired (Action, DispatcherPriority** *[optional]***)**

Executes an action asynchronuously on the UI thread if necessary.

**InvokeIfRequired (Action, DispatcherPriority** *[optional]***)**

Executes an action synchronuously on the UI thread if necessary.

### Usage

Provide a dispatcher in the constructor or use the static property *Default* to use the default application dispatcher.

``` CSharp
DispatcherWrapper.Default.InvokeIfRequired(() => (RandomValuesCommand as ActionCommand).RaiseCanExecuteChanged());
```
