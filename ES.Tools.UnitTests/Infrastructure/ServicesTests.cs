using System;
using ES.Tools.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ES.Tools.UnitTests.Infrastructure
{
  /// <summary>
  /// Unit tests testing the <see cref="Services"/>
  /// </summary>
  public class ServicesFactoryTests
  {
    [TearDown]
    public void Cleanup()
    {
      Services.Instance.Clear();
    }

    [Test]
    public void RegisterServiceTest()
    {
      Services.Instance.RegisterService<IService1>(new Service1());
      Assert.That(Services.Instance.HasService<IService1>(), Is.True);
    }

    [Test]
    public void UnRegisterServiceTest()
    {
      Services.Instance.RegisterService<IService1>(new Service1());
      Services.Instance.UnregisterService<IService1>();
      Assert.That(Services.Instance.HasService<IService1>(), Is.False);
    }

    [Test]
    public void HasServiceTest()
    {
      Services.Instance.RegisterService<IService1>(new Service1());
      Assert.That(Services.Instance.HasService<IService1>(), Is.True);
      Assert.That(Services.Instance.HasService<IService2>(), Is.False);
    }

    [Test]
    public void GetServiceTest()
    {
      var service1 = new Service1();
      Services.Instance.RegisterService<IService1>(service1);
      Assert.That(Services.Instance.GetService<IService1>(), Is.SameAs(service1));
    }


    [Test]
    public void TryGetServiceTest()
    {
      var service1 = new Service1();
      Services.Instance.RegisterService<IService1>(service1);
      bool result1 = Services.Instance.TryGetService(out IService1 resultService1);
      Assert.That(resultService1, Is.SameAs(service1));
      Assert.That(result1, Is.True);

      bool result2 = Services.Instance.TryGetService(out IService2 resultService2);
      Assert.That(resultService2, Is.Null);
      Assert.That(result2, Is.False);
    }

    [Test]
    public void ListServicesTest()
    {
      Services.Instance.RegisterService<IService1>(new Service1());
      Services.Instance.RegisterService<IService2>(new Service2());
      Assert.That(Services.Instance.ListServices(), Contains.Item(typeof(IService1)));
      Assert.That(Services.Instance.ListServices(), Contains.Item(typeof(IService2)));
    }

    [Test]
    public void RegisterSameViewModelTwiceTest()
    {
      Services.Instance.RegisterService<IService1>(new Service1());
      Assert.Throws<InvalidOperationException>(() => Services.Instance.RegisterService<IService1>(new Service1()));
    }

    [Test]
    public void GetUnknownServiceTest()
    {
      Assert.Throws<InvalidOperationException>(() => Services.Instance.GetService<IService2>());
    }

    [Test]
    public void TemporaryReplaceServiceTest()
    {
      // Register default service
      var service1 = new Service1();
      Services.Instance.RegisterService<IService1>(service1);
      Assert.That(Services.Instance.GetService<IService1>(), Is.SameAs(service1));

      // Replace by temporary replacement
      var replacement = new UnitTestService1();
      using (var replacer = new TempServiceReplacement<IService1>(replacement))
      {
        Assert.That(Services.Instance.GetService<IService1>(), Is.SameAs(replacement));
      }

      Assert.That(Services.Instance.GetService<IService1>(), Is.SameAs(service1));
    }

    internal interface IService1
    {
    }

    internal interface IService2
    {
    }

    internal class Service1 : IService1
    {
    }

    internal class UnitTestService1 : IService1
    {
    }

    internal class Service2 : IService2
    {
    }
  }
}
