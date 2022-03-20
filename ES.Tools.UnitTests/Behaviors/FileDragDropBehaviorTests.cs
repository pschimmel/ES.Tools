using System.Threading;
using System.Windows.Controls;
using ES.Tools.Behaviors;
using Moq;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Behaviors
{
  /// <summary>
  /// Unit tests testing the <see cref="FileDragDropBehavior"/>
  /// </summary>
  public class FileDragDropBehaviorTests
  {
    private TextBox _textBox;
    private IFileDragDropTarget _target;

    [SetUp]
    public void Setup()
    {
      _textBox = new TextBox() { AllowDrop = true };
      _target = Mock.Of<IFileDragDropTarget>();
      FileDragDropBehavior.SetIsFileDragDropEnabled(_textBox, true);
      FileDragDropBehavior.SetFileDragDropTarget(_textBox, _target);
    }

    /// <summary>
    /// Tests if the attached behavior values are set.
    /// </summary>
    [Test, Apartment(ApartmentState.STA)]
    public void FileDragDropBehaviorTest()
    {
      Assert.IsTrue(FileDragDropBehavior.GetIsFileDragDropEnabled(_textBox));
      Assert.That(FileDragDropBehavior.GetFileDragDropTarget(_textBox), Is.EqualTo(_target));
    }
  }
}