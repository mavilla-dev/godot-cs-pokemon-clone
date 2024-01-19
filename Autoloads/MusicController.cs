using Godot;

public interface IMusicController {
  public AudioStreamMP3 IntroMusic { get; }
  public AudioStreamPlayer2D Player { get; }
}

public partial class MusicController : Node, IMusicController {
  [Export] public AudioStreamMP3 IntroMusic { get; set; }

  public AudioStreamPlayer2D Player { get; set; }

  public override void _Ready() {
    Player = GetNode<AudioStreamPlayer2D>("%Player");
  }
}
