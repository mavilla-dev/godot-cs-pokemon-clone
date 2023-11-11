using System.Linq;
using Godot;

public interface ITileMapController {
  public MapName MapName { get; set; }
  public MapData TileMap { get; set; }
}

public partial class TileMapController : Node, ITileMapController {
  public MapName MapName { get; set; }
  public MapData TileMap { get; set; }
}
