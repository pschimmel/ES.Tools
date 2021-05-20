using ES.Tools.Core.Infrastructure;
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
      // Not a real test, but better than nothing. The unit test will not in design time.
      Assert.AreEqual(false, Utilities.IsDesignTime);
    }
  }
}