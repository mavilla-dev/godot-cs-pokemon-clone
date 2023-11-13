using Godot;

public partial class PlayerStartMenu : Control {
  [Export] private PackedScene PokemonViewScene { get; set; }

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
      QueueFree();
      AcceptEvent();
    }
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

  private void HandleSaveClick() {
    GD.Print("save click");
  }

  private void HandleOptionsClick() {
    GD.Print("Options click");
  }

  private void HandleExitClick() {
    QueueFree();
  }

  private void HandleQuitGame() {
    GetTree().Quit();
  }
}
