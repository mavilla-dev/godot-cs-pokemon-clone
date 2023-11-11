using Godot;

public partial class MapResource : Resource {
  [Export] public MapName MapName { get; set; }
  [Export] public Resource MapScene { get; set; }
}
