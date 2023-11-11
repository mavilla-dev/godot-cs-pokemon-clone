using System.Linq;
using Godot;
using static Godot.Node;

public class GameWorldManager {
  private readonly Node _root;
  private readonly PlayerGridMovementController _playerController;
  private readonly ITileMapController _mapController;
  private readonly MapDatabase _mapDatabase;

  public GameWorldManager(
    Node root,
    PlayerGridMovementController playerController,
    ResourceDatabase database,
    ITileMapController mapController) {
    _root = root;
    _playerController = playerController;
    _mapController = mapController;
    _mapDatabase = database.MapDb;
  }

  public void SwapGameScene(MapName mapName, Vector2I gridLocation) {
    MapData map = SetupMapPortion(mapName);

    var localPos = map.MapToLocal(gridLocation);
    _playerController.Character.GlobalPosition = map.ToGlobal(localPos);
    _playerController.Character.ProcessMode = ProcessModeEnum.Always;
  }

  public void SwapGameScene(MapName mapName, TeleportKey targetKey) {
    MapData map = SetupMapPortion(mapName);

    var targetArea = map.TeleportAreas.FirstOrDefault(x => x.Key == targetKey);
    _playerController.Character.GlobalPosition = targetArea.SpawnLocation.GlobalPosition;
    _playerController.Character.ProcessMode = ProcessModeEnum.Always;
  }

  public void ClearActiveMap() {
    foreach (var child in _root.GetChildren()) {
      _root.RemoveChild(child);
      child.QueueFree();
    }
  }

  private MapData SetupMapPortion(MapName mapName) {
    MapResource mapResource = _mapDatabase.Maps.FirstOrDefault(x => x.MapName == mapName);
    if (mapResource == null) {
      GD.PrintErr("Map could not be found: " + mapName);
      return null;
    }

    _playerController.Character.ProcessMode = ProcessModeEnum.Disabled;
    ClearActiveMap();

    var packedScene = GD.Load<PackedScene>(mapResource.MapScene.ResourcePath);
    var mapData = packedScene.Instantiate<MapData>();
    mapData.OnRequestTeleport += HandleRequestTeleport;

    _root.AddChild(mapData);
    _playerController.ActiveTileMap = mapData;

    _mapController.TileMap = mapData;
    _mapController.MapName = mapName;

    return mapData;
  }

  private async void HandleRequestTeleport(int targetMap, int targetKey) {
    MapName mapName = (MapName)targetMap;
    TeleportKey teleportKey = (TeleportKey)targetKey;

    if (mapName == MapName.None || teleportKey == TeleportKey.None) {
      return;
    }

    if (_playerController.IsCharacterMoving) {
      // Wait for movement to finish
      await _root.ToSignal(_playerController, nameof(_playerController.OnFinishedMoving));
    }

    SwapGameScene(mapName, teleportKey);
  }
}
