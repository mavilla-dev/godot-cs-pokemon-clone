
using System.Linq;
using Godot;
using static Godot.Node;

public class GameWorldManager {
  private readonly Node _root;
  private readonly PlayerGridMovementController _moveController;
  private readonly MapDatabase _mapDatabase;

  public GameWorldManager(
    Node root,
    PlayerGridMovementController moveController,
    ResourceDatabase database) {
    _root = root;
    _moveController = moveController;
    _mapDatabase = database.AllMaps;
  }

  public void SwapGameScene(Resource mapResource, TeleportKey targetKey) {
    _moveController.Character.ProcessMode = ProcessModeEnum.Disabled;
    ClearActiveMap();

    var packedScene = GD.Load<PackedScene>(mapResource.ResourcePath);
    var mapData = packedScene.Instantiate<MapData>();
    mapData.OnRequestTeleport += HandleRequestTeleport;

    _root.AddChild(mapData);
    _moveController.ActiveTileMap = mapData;

    var targetArea = mapData.TeleportAreas.FirstOrDefault(x => x.Key == targetKey);
    _moveController.Character.GlobalPosition = targetArea.SpawnLocation.GlobalPosition;
    _moveController.Character.ProcessMode = ProcessModeEnum.Always;
  }

  public void ClearActiveMap() {
    foreach (var child in _root.GetChildren()) {
      _root.RemoveChild(child);
      child.QueueFree();
    }
  }

  private async void HandleRequestTeleport(int targetMap, int targetKey) {
    MapName mapKey = (MapName)targetMap;
    TeleportKey teleportKey = (TeleportKey)targetKey;

    if (mapKey == MapName.None || teleportKey == TeleportKey.None) {
      return;
    }

    if (_moveController.IsCharacterMoving) {
      // Wait for movement to finish
      await _root.ToSignal(_moveController, nameof(_moveController.OnFinishedMoving));
    }

    MapResource map = _mapDatabase.Maps.FirstOrDefault(x => x.MapKey == mapKey);
    if (map == null) {
      GD.PrintErr("Could not find map with key: " + mapKey);
      return;
    }

    SwapGameScene(map.MapScene, teleportKey);
  }
}
