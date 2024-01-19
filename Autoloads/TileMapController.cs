using System.Linq;
using Godot;

public interface ITileMapController {
  public MapName MapName { get; set; }
  public MapData ActiveTileMap { get; set; }
  public Character Character { get; }

  public void LoadSavedGame(ISaveData saveData);
  public void LoadStartingZone();
  public void SwapTilemapAsync(MapName targetMap, TeleportKey targetLocation);
  public Vector2I GetPlayerGridPosition();
}

public partial class TileMapController : Node, ITileMapController {
  [Export] private MapDatabase _mapDatabase;
  [Export] private PackedScene _characterScene;

  public MapName MapName { get; set; }
  public MapData ActiveTileMap { get; set; }
  public Character Character { get; set; }

  private Node _root;

  #region Lifecycle
  public override void _Ready() {
    _root = GetTree().Root;
  }

  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed("left_click")) {
      if (ActiveTileMap == null) return;

      var mouseClick = (ev as InputEventMouse).GlobalPosition;
      var cords = ActiveTileMap.LocalToMap(ActiveTileMap.ToLocal(mouseClick));
      var tiledata = ActiveTileMap.GetCellTileData(Constants.TileMapLayer.WALKABLE, cords);
      GD.Print("Looking at Tilemap " + cords);
      GD.Print("is_walkable " + tiledata != null);
      GD.Print("is_wall " + tiledata?.GetCustomData(Constants.TileMapProperties.IS_WALL).AsBool());
      GD.Print("is_occupied " + tiledata?.GetCustomData(Constants.TileMapProperties.IS_OCCUPIED).AsBool());
    }
  }
  #endregion Lifecycle

  #region Interface API

  public Vector2I GetPlayerGridPosition() {
    var charLocalPos = ActiveTileMap.ToLocal(Character.GlobalPosition);
    return ActiveTileMap.LocalToMap(charLocalPos);
  }

  public async void SwapTilemapAsync(MapName targetMap, TeleportKey targetLocation) {
    await ToSignal(Autoload.MapController.Character, nameof(Autoload.MapController.Character.OnFinishedMoving));
    Autoload.MapController.Character.DisableMovement(true);
    Autoload.AnimationController.CanvasLayer.Show();
    Autoload.AnimationController.AnimationPlayer.Play("FadeToBlack");
    await ToSignal(Autoload.AnimationController.AnimationPlayer, "animation_finished");
    ActiveTileMap.Free();

    IMapResource mapResource = GetMap(targetMap);
    var newMap = mapResource.MapScene.Instantiate<MapData>();
    _root.AddChild(newMap);

    var location = newMap.TeleportAreas.FirstOrDefault(x => x.Key == targetLocation);
    Character.GlobalPosition = location.SpawnLocation.GlobalPosition;

    ActiveTileMap = newMap;
    MapName = targetMap;
    Autoload.AnimationController.AnimationPlayer.PlayBackwards("FadeToBlack");
    await ToSignal(Autoload.AnimationController.AnimationPlayer, "animation_finished");
    Autoload.AnimationController.CanvasLayer.Hide();
    Character.DisableMovement(false);
  }

  public void LoadSavedGame(ISaveData saveData) {
    var mapResource = GetMap(saveData.ActiveMap);

    var tilemap = mapResource.MapScene.Instantiate<MapData>();
    _root.AddChild(tilemap);
    var character = _characterScene.Instantiate<Character>();
    _root.AddChild(character);

    var localPosition = tilemap.MapToLocal(saveData.PlayerGridLocation);
    character.GlobalPosition = tilemap.ToGlobal(localPosition);
    character.DisableMovement(false);

    MapName = saveData.ActiveMap;
    ActiveTileMap = tilemap;
    Character = character;
  }

  public void LoadStartingZone() {
    var startingZone = MapName.PalletTown_MyHouse_UpperLevel;
    var mapResource = GetMap(startingZone);

    var tilemap = mapResource.MapScene.Instantiate<MapData>();
    _root.AddChild(tilemap);
    var character = _characterScene.Instantiate<Character>();
    _root.AddChild(character);

    WorldTeleport tpData = tilemap.TeleportAreas.First(x => x.Key == TeleportKey.PalletTown_MyHouse_InitialSpawn);
    character.GlobalPosition = tpData.SpawnLocation.GlobalPosition;
    character.DisableMovement(false);

    ActiveTileMap = tilemap;
    MapName = startingZone;
    Character = character;
  }

  #endregion Interface API

  private IMapResource GetMap(MapName mapName) {
    return _mapDatabase.GetMapOrDefault(mapName);
  }
}
