using System.Linq;
using Godot;

public partial class MapDatabase : Resource {
  [Export] private MapResource[] _maps;

  public IMapResource GetMapOrDefault(MapName mapName) {
    return _maps.FirstOrDefault(x => x.MapName == mapName);
  }
}
