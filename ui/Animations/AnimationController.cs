using Godot;

public interface IAnimationController {
  AnimationPlayer AnimationPlayer { get; }
  public CanvasLayer CanvasLayer { get; }
}

public partial class AnimationController : Node, IAnimationController {
  public AnimationPlayer AnimationPlayer { get; private set; }
  public CanvasLayer CanvasLayer { get; set; }

  public override void _Ready() {
    AnimationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
    CanvasLayer = GetNode<CanvasLayer>("%CanvasLayer");

    CanvasLayer.Hide();
  }
}
