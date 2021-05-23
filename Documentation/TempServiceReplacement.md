# TempServiceReplacement
Namespace: **ES.Tools.Core.Infrastructure** *(ES.Tools.Core)*

**TempServiceReplacement** is a temporary replacement for services. This can be used for example in unit tests.
This implements *IDisposable*. As soon as the instance is disposed, the previously registered service is restored.

```CSharp
// Register default service
var service1 = new Service1();
Services.Instance.RegisterService<IService1>(service1);

// Replace by temporary replacement
var replacement = new UnitTestService1();
using (var replacer = new TempServiceReplacement<IService1>(replacement))
{
  // Do something
}
```

### Remarks

>See also [*Services*](Services.md).