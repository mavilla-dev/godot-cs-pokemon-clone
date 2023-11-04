using Godot;

public partial class StartMenuPokemon : Control {

  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.CANCEL)) {
      QueueFree();
    }

    AcceptEvent();
  }
}
