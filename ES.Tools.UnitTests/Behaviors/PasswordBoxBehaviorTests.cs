using System.Reflection;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Documents;
using ES.Tools.Behaviors;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Behaviors
{
  /// <summary>
  /// Unit tests testing the <see cref="PasswordBoxBehavior"/>
  /// </summary>
  public class PasswordBoxBehaviorTests
  {
    private const string _sampleText = "This is a sample text.";
    private PasswordBox _passwordBox;

    [SetUp]
    public void Setup()
    {
      _passwordBox = new PasswordBox();
      _passwordBox.Password = _sampleText;
      PasswordBoxBehavior.SetSelectAllTextOnFocus(_passwordBox, true);
    }

    /// <summary>
    /// Tests if the attached behavior value is set.
    /// </summary>
    [Test, Apartment(ApartmentState.STA)]
    public void PasswordBoxBehaviorBasicTest()
    {
      Assert.IsTrue(PasswordBoxBehavior.GetSelectAllTextOnFocus(_passwordBox));
    }

    /// <summary>
    /// Tests if all text is selected after a <see cref="PasswordBox"/> is focused.
    /// </summary>
    [Test, Apartment(ApartmentState.STA)]
    public void TextBoxBehaviorSelectAllTextOnFocusTest()
    {
      var selectionPI = typeof(PasswordBox).GetProperty("Selection", BindingFlags.Instance | BindingFlags.NonPublic);
      var selection = (TextSelection)selectionPI.GetMethod.Invoke(_passwordBox, null);
      Assert.IsTrue(selection.IsEmpty);
      _passwordBox.Focus();
      Assert.IsFalse(selection.IsEmpty);
    }
  }
}