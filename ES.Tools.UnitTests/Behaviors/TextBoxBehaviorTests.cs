using System.Threading;
using System.Windows.Controls;
using ES.Tools.Behaviors;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Behaviors
{
  /// <summary>
  /// Unit tests testing the <see cref="TextBoxBehavior"/>
  /// </summary>
  public class TextBoxBehaviorTests
  {
    private const string _sampleText = "This is a sample text.";
    private TextBox _textBox;

    [SetUp]
    public void Setup()
    {
      _textBox = new TextBox();
      _textBox.Text = _sampleText;
      TextBoxBehavior.SetSelectAllTextOnFocus(_textBox, true);
    }

    /// <summary>
    /// Tests if the attached behavior value is set.
    /// </summary>
    [Test, Apartment(ApartmentState.STA)]
    public void TextBoxBehaviorBasicTest()
    {
      Assert.IsTrue(TextBoxBehavior.GetSelectAllTextOnFocus(_textBox));
    }

    /// <summary>
    /// Tests if all text is selected after a <see cref="TextBox"/> is focused.
    /// </summary>
    [Test, Apartment(ApartmentState.STA)]
    public void TextBoxBehaviorSelectAllTextOnFocusTest()
    {
      Assert.AreEqual(string.Empty, _textBox.SelectedText);
      _textBox.Focus();
      Assert.AreEqual(_sampleText, _textBox.SelectedText);
    }
  }
}