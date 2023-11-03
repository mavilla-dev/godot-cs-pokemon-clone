using Godot;

public partial class Main : Node2D {
  [ExportGroup("Scenes")]
  [Export] public Resource StartScreenScene { get; set; }
  [Export] public Resource LoadGameScene { get; set; }
  [Export] public Resource StartingZoneScene { get; set; }
  [Export] public Resource CharacterScene { get; set; }
  [Export] public Resource StartMenuScene { get; set; }

  [ExportGroup("Nodes")]
  [Export] public Node ActiveSceneRoot { get; set; }
  [Export] public CanvasLayer GameUiRoot { get; set; }

  private GameWorldManager _gameWorldManager;
  private UiManager _uiManager;

  public override void _Ready() {
    _gameWorldManager = new GameWorldManager(ActiveSceneRoot, MoveController, Database);
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

  private void HandleGameLoadSelected() {
    SetupCharacterNode();

    var isNewGame = true;
    if (isNewGame) {
      HandleNewGameSetup();
    } else {
      HandleLoadedGameSetup();
    }

    MoveController.IsOverworldActive = true;
  }

  private void SetupCharacterNode() {
    var startMenuNode = _uiManager.SwapUi<StartMenu>(StartMenuScene);
    startMenuNode.HideControl();
    MoveController.StartMenu = startMenuNode;
    startMenuNode.OnClose += MoveController.HandleClosingStartMenu;

    var charNode = MakeResource<Character>(CharacterScene);
    AddChild(charNode);
    MoveController.Character = charNode;
  }

  private void HandleNewGameSetup() {
    _gameWorldManager.SwapGameScene(StartingZoneScene, TeleportKey.PalletTown_MyHouse_InitialSpawn);
  }

  private void HandleLoadedGameSetup() {
    //todo
  }

  #endregion Load Game Scene

  #region AutoLoads

  private PlayerGridMovementController MoveController
    => GetNode<PlayerGridMovementController>("/root/PlayerGridMovementController");

  private ResourceDatabase Database
    => GetNode<ResourceDatabase>("/root/ResourceDatabase");

  #endregion AutoLoads

}
