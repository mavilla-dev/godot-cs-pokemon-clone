using Godot;

public partial class MusicArea : Area2D {
  [Export] private AudioStreamMP3 MusicForArea { get; set; }

  public override void _Ready() {
    AreaEntered += PlayMusic;
  }

  private void PlayMusic(Area2D area) {
    if (area.Name == "Character" && Autoload.MusicController.Player.Stream != MusicForArea) {
      Autoload.MusicController.Player.Stop();
      Autoload.MusicController.Player.Stream = MusicForArea;
      Autoload.MusicController.Player.Play(0);
    }
  }

}
