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
      Services.Instance.RegisterService(new Service1());
      Assert.That(Services.Instance.HasService<Service1>(), Is.True);
    }

    [Test]
    public void UnRegisterServiceTest()
    {
      Services.Instance.RegisterService(new Service1());
      Services.Instance.UnregisterService<Service1>();
      Assert.That(Services.Instance.HasService<Service1>(), Is.False);
    }

    [Test]
    public void HasServiceTest()
    {
      Services.Instance.RegisterService(new Service1());
      Assert.That(Services.Instance.HasService<Service1>(), Is.True);
      Assert.That(Services.Instance.HasService<Service2>(), Is.False);
    }

    [Test]
    public void GetServiceTest()
    {
      var service1 = new Service1();
      Services.Instance.RegisterService(service1);
      Assert.That(Services.Instance.GetService<Service1>(), Is.SameAs(service1));
    }


    [Test]
    public void TryGetServiceTest()
    {
      var service1 = new Service1();
      Services.Instance.RegisterService(service1);
      bool result1 = Services.Instance.TryGetService(out Service1 resultService1);
      Assert.That(resultService1, Is.SameAs(service1));
      Assert.That(result1, Is.True);

      bool result2 = Services.Instance.TryGetService(out Service2 resultService2);
      Assert.That(resultService2, Is.Null);
      Assert.That(result2, Is.False);
    }

    [Test]
    public void ListServicesTest()
    {
      Services.Instance.RegisterService(new Service1());
      Services.Instance.RegisterService(new Service2());
      Assert.That(Services.Instance.ListServices(), Contains.Item(typeof(Service1)));
      Assert.That(Services.Instance.ListServices(), Contains.Item(typeof(Service2)));
    }

    [Test]
    public void RegisterSameViewModelTwiceTest()
    {
      Services.Instance.RegisterService(new Service1());
      Assert.Throws<InvalidOperationException>(() => Services.Instance.RegisterService(new Service1()));
    }

    [Test]
    public void GetUnknownServiceTest()
    {
      Assert.Throws<InvalidOperationException>(() => Services.Instance.GetService<Service2>());
    }

    internal class Service1
    {
    }

    internal class Service2
    {
    }
  }
}
