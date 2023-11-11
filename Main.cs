using System.Linq;
using Godot;

public partial class Main : Node2D {
  [ExportGroup("Scenes")]
  [Export] public Resource StartScreenScene { get; set; }
  [Export] public Resource LoadGameScene { get; set; }
  [Export] public Resource CharacterNameScene { get; set; }
  [Export] public Resource CharacterScene { get; set; }
  [Export] public Resource StartMenuScene { get; set; }

  [ExportGroup("StartMenu Sub-scenes")]
  [Export] public Resource StartMenuPokemon { get; set; }

  [ExportGroup("Nodes")]
  [Export] public Node ActiveSceneRoot { get; set; }
  [Export] public CanvasLayer GameUiRoot { get; set; }

  private GameWorldManager _gameWorldManager;
  private UiManager _uiManager;

  public override void _Ready() {
    _gameWorldManager = new GameWorldManager(ActiveSceneRoot, MoveController, Database, MapController);
    _uiManager = new UiManager(GameUiRoot);

    LoadStartScreenScene();
  }

  #region Start Screen Methods

  private T MakeResource<T>(Resource resource) where T : Node {
    var packedScene = GD.Load<PackedScene>(resource.ResourcePath);
    return packedScene.Instantiate<T>();
  }

  private void LoadStartScreenScene() {
    // first await screen transition
    var startScreen = _uiManager.SwapUi<StartScreenScene>(StartScreenScene);

    startScreen.OnNextScene += LoadGameSaveLoadScene;
    startScreen.OnCancel += HandleStartScreenCancel;
  }

  private void HandleStartScreenToLoadScene() {
    // first await screen transition
    _uiManager.ClearActiveUi();
    LoadGameSaveLoadScene();
  }

  private void HandleStartScreenCancel() {
    GetTree().Quit();
  }

  #endregion Start Screen Methods

  #region Load Game Scene

  private void LoadGameSaveLoadScene() {
    // first await screen transition
    var loadScene = _uiManager.SwapUi<LoadGameScene>(LoadGameScene);
    loadScene.OnLoadSelected += HandleGameLoadSelected;
    loadScene.OnCancel += LoadStartScreenScene;
  }

  private void HandleGameLoadSelected(int saveSlotIndex) {
    var slotExists = SaveController.GetSaveSlots().Any(x => x.SaveSlotId == saveSlotIndex);

    if (slotExists) {
      SaveController.SelectedSaveSlotIndex = saveSlotIndex;
      HandleLoadedGameSetup();
    } else {
      SaveController.PopulateSlot(saveSlotIndex);
      HandleNewGameSetup();
    }
  }

  private void AllowCharacterMovement() {
    MoveController.IsOverworldActive = true;
    MoveController.DisableMovement(false);
  }

  private void SetupCharacterNode() {
    var startMenuNode = _uiManager.SwapUi<StartMenu>(StartMenuScene);
    startMenuNode.HideControl();
    MoveController.StartMenu = startMenuNode;
    startMenuNode.OnClose += MoveController.HandleClosingStartMenu;
    startMenuNode.OnOptionSelected += HandleStartMenuOptionSelected;

    var charNode = MakeResource<Character>(CharacterScene);
    AddChild(charNode);
    MoveController.Character = charNode;
  }

  private void HandleStartMenuOptionSelected(int startMenuOption) {
    StartMenuOption option = (StartMenuOption)startMenuOption;
    if (option == StartMenuOption.Pokemon) {
      _uiManager.AddUi<StartMenuPokemon>(StartMenuPokemon);
    }
    if (option == StartMenuOption.Save) {
      SaveController.SaveGame();
    }
    if (option == StartMenuOption.Quit) {
      GetTree().Quit();
    }
  }

  private void HandleNewGameSetup() {
    var scene = _uiManager.SwapUi<CharacterNameScene>(CharacterNameScene);
    scene.TreeExited += () => {
      SetupCharacterNode();
      _gameWorldManager.SwapGameScene(
        MapName.PalletTown_MyHouse_UpperLevel,
        TeleportKey.PalletTown_MyHouse_InitialSpawn);
      AllowCharacterMovement();
    };
  }

  private void HandleLoadedGameSetup() {
    var saveData = SaveController.GetActiveSave();
    SetupCharacterNode();
    _gameWorldManager.SwapGameScene(saveData.ActiveMap, saveData.PlayerGridLocation);
    AllowCharacterMovement();
  }

  #endregion Load Game Scene

  #region AutoLoads

  private PlayerGridMovementController MoveController
    => this.GetPlayerGridMovementController();

  private ResourceDatabase Database
    => this.GetResourceDatabase();

  private MapResource[] MapDatabase => this.GetResourceDatabase().MapDb.Maps;

  private ISaveController SaveController
    => this.GetSaveDataManager();

  private ITileMapController MapController => this.GetTileMapController();

  #endregion AutoLoads

}
