using Godot;

public partial class Character : Node2D {
  [Export] public ShapeCast2D ShapeCast2D;

  [Export] private AnimatedSprite2D _animator;

  public override void _Ready() {
    Scale = Constants.PIXEL_SCALE;
  }

  public void AnimateWalk(MoveDirection direction) {
    switch (direction) {
      case MoveDirection.Up:
        _animator.Animation = "walk_up";
        _animator.Play();
        return;

      case MoveDirection.Down:
        _animator.Animation = "walk_down";
        _animator.Play();
        return;

      case MoveDirection.Left:
        _animator.Animation = "walk_left";
        _animator.Play();
        return;

      case MoveDirection.Right:
        _animator.Animation = "walk_right";
        _animator.Play();
        return;
    }
  }

  public void AnimateIdle(MoveDirection direction) {
    switch (direction) {
      case MoveDirection.Up:
        _animator.Animation = "idle_up";
        _animator.Play();
        return;

      case MoveDirection.Down:
        _animator.Animation = "idle_down";
        _animator.Play();
        return;

      case MoveDirection.Left:
        _animator.Animation = "idle_left";
        _animator.Play();
        return;

      case MoveDirection.Right:
        _animator.Animation = "idle_right";
        _animator.Play();
        return;
    }
  }
}
