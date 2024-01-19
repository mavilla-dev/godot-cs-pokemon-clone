using Godot;

public partial class PlayerStartMenu : Control {
  [Export] private PackedScene PokemonViewScene { get; set; }
  [Export] private PackedScene MessageBoxScene { get; set; }
  [Export] private PackedScene ConfirmationModalScene { get; set; }

  private Node _entryRoot;
  private Node _submenuRoot;
  private PlayerStartMenuEntry _pokedex;
  private PlayerStartMenuEntry _pokemon;
  private PlayerStartMenuEntry _items;
  private PlayerStartMenuEntry _playerName;
  private PlayerStartMenuEntry _save;
  private PlayerStartMenuEntry _options;
  private PlayerStartMenuEntry _exit;
  private PlayerStartMenuEntry _quitGame;

  #region Lifecycle

  public override void _Ready() {
    _entryRoot = GetNode("%EntryRoot");
    _submenuRoot = GetNode("%SubMenuRoot");

    _pokedex = _entryRoot.GetNode<PlayerStartMenuEntry>("PokeDex");
    _pokedex.Pressed += HandlePokdexClick;
    _pokedex.GrabFocus();

    _pokemon = _entryRoot.GetNode<PlayerStartMenuEntry>("Pokemon");
    _pokemon.Pressed += HandlePokemonClick;

    _items = _entryRoot.GetNode<PlayerStartMenuEntry>("Items");
    _items.Pressed += HandleItemsClick;

    _playerName = _entryRoot.GetNode<PlayerStartMenuEntry>("PlayerName");
    _playerName.Pressed += HandlePlayerNameClick;

    _save = _entryRoot.GetNode<PlayerStartMenuEntry>("Save");
    _save.Pressed += HandleSaveClick;

    _options = _entryRoot.GetNode<PlayerStartMenuEntry>("Options");
    _options.Pressed += HandleOptionsClick;

    _exit = _entryRoot.GetNode<PlayerStartMenuEntry>("Exit");
    _exit.Pressed += HandleExitClick;

    _quitGame = _entryRoot.GetNode<PlayerStartMenuEntry>("Quit Game");
    _quitGame.Pressed += HandleQuitGame;
  }

  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.UI_CANCEL)) {
      HideMenu();
      AcceptEvent();
    }
  }

  public void SetFocusOnFirstItem() {
    _pokedex.GrabFocus();
  }

  #endregion Lifecycle

  private void HandlePokdexClick() {
    GD.Print("pokedex click");
  }

  private void HandlePokemonClick() {
    var newControl = PokemonViewScene.Instantiate<Control>();
    _submenuRoot.AddChild(newControl);
    newControl.TreeExited += () => {
      _pokemon.GrabFocus();
    };
  }

  private void HandleItemsClick() {
    GD.Print("items click");
  }

  private void HandlePlayerNameClick() {
    GD.Print("playername click");
  }

  private async void HandleSaveClick() {
    // Prompt to confirm save
    var messageBox = MessageBoxScene.Instantiate<MessageBox>();
    GetTree().Root.AddChild(messageBox);
    messageBox.SetText(new string[] { "Are you sure you want to save?" });
    messageBox.Play();
    await ToSignal(messageBox, nameof(messageBox.OnDone));
    await ToSignal(GetTree().CreateTimer(1), "timeout");

    var confirmModal = ConfirmationModalScene.Instantiate<ConfirmationModal>();
    GetTree().Root.AddChild(confirmModal);
    var signal = await ToSignal(confirmModal, nameof(confirmModal.OnSelection));
    var isConfirm = signal[0].AsBool();
    confirmModal.QueueFree();

    if (!isConfirm) {
      messageBox.QueueFree();
      return;
    }

    messageBox.SetText(new string[] { "Saving..." });
    messageBox.Play();

    var success = Autoload.SaveDataController.SaveGame(new SaveGameArgs {
      CurrentMapName = Autoload.MapController.MapName,
      GlobalCharacterLocation = Autoload.MapController.GetPlayerGridPosition(),
    });

    messageBox.SetText(new string[] { isConfirm ? "Game Saved!" : "Could not save..." });
    messageBox.Play();
    await ToSignal(messageBox, nameof(messageBox.OnDone));
    messageBox.QueueFree();
    _save.GrabFocus();
  }

  private void HandleOptionsClick() {
    GD.Print("Options click");
  }

  private void HandleExitClick() {
    HideMenu();
  }

  private void HandleQuitGame() {
    GetTree().Quit();
  }

  private void HideMenu() {
    Hide();
    Autoload.MapController.Character.DisableMovement(false);
  }
}
