using Godot;

public partial class Npc : Node2D {

  private AnimatedSprite2D AnimatedSprite2D { get; set; }

  public override void _Ready() {
    AnimatedSprite2D = GetChild<AnimatedSprite2D>(0);
  }
}
