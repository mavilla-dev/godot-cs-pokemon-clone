using System.Linq;
using Godot;

public partial class Program : Node2D {
  [Export] private PackedScene SceneStartScreen;
  [Export] private PackedScene SceneLoadSave;
  [Export] private PackedScene CharacterScene;

  private Node _canvasLayer;
  private Node _activeUi;

  #region Lifecycle
  public override void _Ready() {
    _canvasLayer = GetNode("%CanvasLayer");
    ChangeSceneToStartScreen();
  }
  #endregion Lifecycle

  private void ChangeSceneToLoadSave() {
    _activeUi?.QueueFree();

    var scene = SceneLoadSave.Instantiate<LoadGameScene>();
    scene.OnGoBack += ChangeSceneToStartScreen;
    scene.OnSaveSelected += HandleLoadSave;

    _canvasLayer.AddChild(scene);
    _activeUi = scene;
  }

  private void HandleLoadSave(int saveSlotId) {
    _activeUi?.QueueFree();

    var saveData = Autoload.SaveDataController.SetActiveSlot(saveSlotId);
    if (saveData.IsNewGame) {
      LoadStartingZone();
    } else {
      LoadSavedGame(saveData);
    }
  }

  private void LoadSavedGame(ISaveData saveData) {
    var mapResource = Autoload.MapController.GetMap(saveData.ActiveMap);

    var tilemap = mapResource.MapScene.Instantiate<MapData>();
    AddChild(tilemap);
    var character = CharacterScene.Instantiate<Character>();
    AddChild(character);

    var localPosition = tilemap.MapToLocal(saveData.PlayerGridLocation);
    character.GlobalPosition = ToGlobal(localPosition);
  }

  private void LoadStartingZone() {
    var mapResource = Autoload.MapController.GetMap(MapName.PalletTown_MyHouse_UpperLevel);

    var tilemap = mapResource.MapScene.Instantiate<MapData>();
    AddChild(tilemap);
    var character = CharacterScene.Instantiate<Character>();
    AddChild(character);

    WorldTeleport tpData = tilemap.TeleportAreas.First(x => x.Key == TeleportKey.PalletTown_MyHouse_InitialSpawn);
    character.GlobalPosition = ToGlobal(tpData.SpawnLocation.GlobalPosition);
  }

  private void ChangeSceneToStartScreen() {
    _activeUi?.QueueFree();

    var scene = SceneStartScreen.Instantiate<StartScreenScene>();
    scene.OnContinue += ChangeSceneToLoadSave;
    scene.OnQuit += () => GetTree().Quit();

    _canvasLayer.AddChild(scene);
    _activeUi = scene;
  }
}
