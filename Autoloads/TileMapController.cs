using Godot;

public interface ITileMapController {
  public MapName MapName { get; set; }
  public MapData TileMap { get; set; }

  public IMapResource GetMap(MapName mapName);
}

public partial class TileMapController : Node, ITileMapController {
  [Export] private MapDatabase _mapDatabase;

  public MapName MapName { get; set; }
  public MapData TileMap { get; set; }

  public IMapResource GetMap(MapName mapName) {
    return _mapDatabase.GetMapOrDefault(mapName);
  }
}
