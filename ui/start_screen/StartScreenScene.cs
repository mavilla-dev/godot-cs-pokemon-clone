using Godot;

public partial class StartScreenScene : Control {
  [Export] private PackedScene LoadGameScene;

  [Signal] public delegate void OnContinueEventHandler();
  [Signal] public delegate void OnQuitEventHandler();

  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.ACCEPT) || ev.IsActionPressed(Constants.InputActions.START)) {
      AcceptEvent();
      EmitSignal(SignalName.OnContinue);
    }

    if (ev.IsActionPressed(Constants.InputActions.CANCEL)) {
      AcceptEvent();
      EmitSignal(SignalName.OnQuit);
    }
  }
}
