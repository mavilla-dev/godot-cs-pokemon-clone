using Godot;

public partial class MapResource : Resource {
  [Export] public MapName MapKey { get; set; }
  [Export] public Resource MapScene { get; set; }
}
