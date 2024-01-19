using Godot;

public partial class ConfirmationModal : CanvasLayer {
  [Signal] public delegate void OnSelectionEventHandler(bool confirm);

  private PlayerStartMenuEntry _entryNo;
  private PlayerStartMenuEntry _entryYes;

  public override void _Ready() {
    _entryNo = GetNode<PlayerStartMenuEntry>("%No");
    _entryYes = GetNode<PlayerStartMenuEntry>("%Yes");

    _entryNo.Pressed += () => EmitSignal(SignalName.OnSelection, false);
    _entryYes.Pressed += () => EmitSignal(SignalName.OnSelection, true);

    _entryNo.GrabFocus();
  }
}
