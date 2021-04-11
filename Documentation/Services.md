# Services
Namespace: **ES.Tools.Infrastructure**

**Services** provides an infrastructure for services. It can be used as simple DI container. It is a singleton  class.

### Methods

**RegisterService&lt;T&gt;(T)**

Register a new service.

**UnregisterService&lt;T&gt;()**

Unregister a previously registered service.

**T GetService&lt;T&gt;()**

Returns a service for a certain type. Throws an *InvalidOperationException* when the requested type is not registered.

**bool TryGetService&lt;T&gt;(out T)**

Tries to get a service for a certain type. Returns true if the type could be retrieved, otherwise false.

**bool HasService&lt;T&gt;()**

Checks if a service for a certain type is registered.

**Type[] ListServices()**

Returns a list of all registered service types.

**Clear()**

Removes all services.

### Usage

Register a service by calling the **RegisterService&lt;T&gt;** method.
```CSharp
var service1 = new Service1();
Services.Instance.RegisterService<IService1>(service1);
bool result1 = Services.Instance.TryGetService(out IService1 resultService1);
```

### Remarks

>See also [*TempServiceReplacement*](TempServiceReplacement).