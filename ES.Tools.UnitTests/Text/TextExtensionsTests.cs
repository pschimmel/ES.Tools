using ES.Tools.Core.Infrastructure;
using ES.Tools.Core.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ES.Tools.UnitTests.Infrastructure
{
  /// <summary>
  /// Unit tests testing the <see cref="Services"/>
  /// </summary>
  public class TextExtensionsTests
  {
    private readonly string _testString = "Test";
    private readonly string _testStringWithBOM = "Test\uFEFF";
    private readonly string _testStringWithZeroWidthSpace = "Test\u200B";

    [TearDown]
    public void Cleanup()
    {
      Services.Instance.Clear();
    }

    [Test]
    public void RegisterServiceTest()
    {
      Assert.AreEqual(_testString, _testString.StripBOM());
      Assert.AreEqual(_testString, _testStringWithBOM.StripBOM());
      Assert.AreEqual(_testString, _testStringWithZeroWidthSpace.StripBOM());
    }
  }
}
