using System.Linq;
using Godot;

public partial class PlayerStartMenu : Control {
  private Node _entryRoot;

  #region Lifecycle
  public override void _Ready() {
    _entryRoot = GetNode<Node>(
      GetMeta("EntryRoot").As<NodePath>()
    );

    var entries = _entryRoot.GetChildren().Select(x => x as PlayerStartMenuEntry).ToList();
    entries.First().GrabFocus();
    entries.First().Pressed += () => GD.Print("Clicked");
  }
  #endregion Lifecycle
}
