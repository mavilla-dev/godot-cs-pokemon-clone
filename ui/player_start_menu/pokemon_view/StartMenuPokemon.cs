using System;
using Godot;

public partial class StartMenuPokemon : Control {
  [Export] private PackedScene PokemonEntryScene;

  private Node _entryRoot;
  private Node _submenuRoot;

  #region Lifecycle

  public override void _Ready() {
    _entryRoot = GetNode("%EntryRoot");

    // ClearTestData();
    PopulateEntriesFromBelt();
    SetFocusToFirstChild();
  }

  private void SetFocusToFirstChild() {
    _entryRoot.GetChild<Control>(0)?.GrabFocus();
  }


  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.UI_CANCEL)) {
      QueueFree();
      AcceptEvent();
    }
  }
  #endregion Lifecycle

  private void ClearTestData() {
    foreach (var child in _entryRoot.GetChildren()) {
      child.Free();
    }
  }

  private void PopulateEntriesFromBelt() {
    var pokeController = Autoload.Instance.GetPokemonController();
    foreach (var item in pokeController.PokemonBelt) {
      var entry = PokemonEntryScene.Instantiate<PokemonBeltEntry>();
      _entryRoot.AddChild(entry);
      entry.SetPokeName(item.Nickname, item.Pokemon.Name);
      // add more fields
    }
  }
}
