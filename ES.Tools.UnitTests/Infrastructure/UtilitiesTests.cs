using ES.Tools.Infrastructure;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Infrastructure
{
  /// <summary>
  /// Unit tests testing the <see cref="Utilities"/> class.
  /// </summary>
  public class UtilitiesTests
  {
    /// <summary>
    /// Tests IsInDesignMode property.
    /// </summary>
    [Test]
    public void IsInDesignModeTest()
    {
      // Not a real test, but better than nothing
      Assert.AreEqual(false, Utilities.IsInDesignMode);
    }
  }
}