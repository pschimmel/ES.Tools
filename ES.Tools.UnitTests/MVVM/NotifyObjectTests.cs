using ES.Tools.Core.MVVM;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ES.Tools.UnitTests.MVVM
{
  /// <summary>
  /// Unit tests testing the <see cref="NotifyObject"/>
  /// </summary>
  public class NotifyObjectTests
  {
    [Test]
    public void NotifyObjectEventTest()
    {
      string notifiedProperty = null;
      var testObject = new TestObject();
      testObject.PropertyChanged += (s, e) => notifiedProperty = e.PropertyName;

      Assert.IsNull(notifiedProperty);
      testObject.Property1 = 2;
      Assert.AreEqual(2, testObject.Property1);
      Assert.AreEqual(nameof(testObject.Property1), notifiedProperty);

      testObject.Property2 = 5d;
      Assert.AreEqual(5d, testObject.Property2);
      Assert.AreEqual(nameof(testObject.Property2), notifiedProperty);

      testObject.Property3 = "Blah";
      Assert.AreEqual("Blah", testObject.Property3);
      Assert.AreEqual(nameof(testObject.Property3), notifiedProperty);
    }
  }

  internal class TestObject : NotifyObject
  {
    private int _property1;
    private double _property2;
    private string _property3;

    public int Property1
    {
      get => _property1;
      set
      {
        _property1 = value;
        OnPropertyChanged(nameof(Property1));
      }
    }

    public double Property2
    {
      get => _property2;
      set
      {
        _property2 = value;
        OnPropertyChanged(() => Property2);
      }
    }

    public string Property3
    {
      get => _property3;
      set
      {
        _property3 = value;
        OnPropertyChanged(nameof(Property3));
      }
    }
  }
}
