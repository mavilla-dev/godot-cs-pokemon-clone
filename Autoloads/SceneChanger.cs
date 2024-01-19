using Godot;

public interface ISceneChanger {
  public PackedScene StartScreen { get; }
  public PackedScene LoadGameScene { get; }
  public PackedScene NameInputScene { get; }
  public void ChangeScene(PackedScene scene);
  public void CloseGame();
}

public partial class SceneChanger : Node2D, ISceneChanger {
  [Export] public PackedScene StartScreen { get; set; }
  [Export] public PackedScene LoadGameScene { get; set; }
  [Export] public PackedScene NameInputScene { get; set; }

  public void ChangeScene(PackedScene scene) {
    GetTree().ChangeSceneToPacked(scene);
  }

  public void CloseGame() {
    GetTree().Quit();
  }
}
