using Godot;

public partial class GridMovement : Node {
  /*
  // Godot doesn't support enum with signal args so
  // casting to int
  [Signal] public delegate void OnRequestMoveEventHandler(int direction);

  public override void _UnhandledInput(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.UP)) {
      EmitMove(MoveDirection.Up);
    }
    if (ev.IsActionPressed(Constants.InputActions.DOWN)) {
      EmitMove(MoveDirection.Down);
    }
    if (ev.IsActionPressed(Constants.InputActions.LEFT)) {
      EmitMove(MoveDirection.Left);
    }
    if (ev.IsActionPressed(Constants.InputActions.RIGHT)) {
      EmitMove(MoveDirection.Right);
    }

    var wasMovementReleased = ev.IsActionReleased(Constants.InputActions.UP)
      || ev.IsActionReleased(Constants.InputActions.DOWN)
      || ev.IsActionReleased(Constants.InputActions.LEFT)
      || ev.IsActionReleased(Constants.InputActions.RIGHT);

    if (wasMovementReleased) {
      EmitMove(MoveDirection.None);
    }
  }

  private void EmitMove(MoveDirection direction) {
    EmitSignal(SignalName.OnRequestMove, (int)direction);
    // GD.Print("Emitting Movement: " + direction);
  }
  */
}

public enum MoveDirection {
  None,
  Up,
  Down,
  Left,
  Right
}
