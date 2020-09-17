using ES.Tools.Helpers;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Helpers
{
  /// <summary>
  /// Unit tests testing the <see cref="Utility"/> class.
  /// </summary>
  public class TreeHelperExtensionsTests
  {
    /// <summary>
    /// Testing IsInDesignMode property.
    /// </summary>
    [Test]
    public void PasswordBoxBehaviorBasicTest()
    {
      // Not a real test, but better than nothing
      Assert.AreEqual(false, Utility.IsInDesignMode);
    }
  }
}