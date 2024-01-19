using Godot;

public partial class Npc : Node2D {

  private AnimatedSprite2D AnimatedSprite2D { get; set; }
  private TileMap _tileMap;

  public override void _Ready() {
    AnimatedSprite2D = GetChild<AnimatedSprite2D>(0);
  }
}
