using ES.Tools.UI;
using NUnit.Framework;

namespace ES.Tools.UnitTests.UI
{
  /// <summary>
  /// Unit tests testing the <see cref="DependencyPropertyWatcher{T}"/> class.
  /// </summary>
  public partial class DependencyPropertyWatcherTests
  {
    /// <summary>
    /// Tests if events are cast correctly by <see cref="DependencyPropertyWatcher{T}"/>.
    /// </summary>
    [Test]
    public void DependencyPropertyWatcherEventTest()
    {
      var testObject = new TestDependencyObject();
      using var watcher = new DependencyPropertyWatcher<string>(testObject, nameof(TestDependencyObject.Text));
      bool success = false;

      watcher.PropertyChanged += (s, e) => success = true;
      Assert.IsFalse(success);

      testObject.Text = "Test";

      Assert.IsTrue(success);
      Assert.AreEqual("Test", watcher.Value);
    }
  }
}