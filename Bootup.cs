using Godot;

public partial class Bootup : Node2D {
  public override void _Ready() {
    Autoload.SceneChanger.ChangeScene(Autoload.SceneChanger.StartScreen);
  }
}
