using Godot;

public interface IMapResource {
  public MapName MapName { get; }
  public PackedScene MapScene { get; }
}

public partial class MapResource : Resource, IMapResource {
  [Export] public MapName MapName { get; set; }
  [Export] public PackedScene MapScene { get; set; }
}
