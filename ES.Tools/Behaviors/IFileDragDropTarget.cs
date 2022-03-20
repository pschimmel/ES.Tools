namespace ES.Tools.Behaviors
{
  /// <summary>
  /// Interface that ViewModels need to implement to act as a target for file Drag-Drop operations.
  /// </summary>
  public interface IFileDragDropTarget
  {
    bool AllowDrop(string[] filePaths);

    void OnFileDrop(string[] filePaths);
  }
}
