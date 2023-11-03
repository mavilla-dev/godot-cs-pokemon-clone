using Godot;

public partial class StartScreenScene : Control {
  [Signal] public delegate void OnNextSceneEventHandler();
  [Signal] public delegate void OnCancelEventHandler();

  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.ACCEPT)
      || ev.IsActionPressed(Constants.InputActions.START)) {
      GD.Print("Next Scene Triggered");
      EmitSignal(SignalName.OnNextScene);
    }

    if (ev.IsActionPressed(Constants.InputActions.CANCEL)) {
      GD.Print("On Cancel Triggered");
      EmitSignal(SignalName.OnCancel);
    }

    AcceptEvent();
  }
}
