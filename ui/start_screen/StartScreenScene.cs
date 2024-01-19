using Godot;

public partial class StartScreenScene : Control {
  [Export] private PackedScene LoadGameScene;

  [Signal] public delegate void OnContinueEventHandler();
  [Signal] public delegate void OnQuitEventHandler();

  public override void _Ready() {
    Autoload.MusicController.Player.Stream = Autoload.MusicController.IntroMusic;
    Autoload.MusicController.Player.Play();
  }

  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.ACCEPT) || ev.IsActionPressed(Constants.InputActions.START)) {
      AcceptEvent();
      Autoload.SceneChanger.ChangeScene(Autoload.SceneChanger.LoadGameScene);
    }

    if (ev.IsActionPressed(Constants.InputActions.CANCEL)) {
      AcceptEvent();
      Autoload.SceneChanger.CloseGame();
    }
  }
}
