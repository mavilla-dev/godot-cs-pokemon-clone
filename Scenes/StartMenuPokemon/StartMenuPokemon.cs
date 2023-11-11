using System.Linq;
using Godot;

public partial class StartMenuPokemon : Control {
  [Export] private Node EntryRoot;

  public override void _Ready() {
    var pokeController = this.GetPokemonController();
    var entries = EntryRoot.GetChildren()
      .Select(x => x as PokemonBeltEntry)
      .ToList();

    for (int index = 0; index < entries.Count; index++) {
      var entry = entries[index];
      var pokemon = pokeController.PokemonBelt.ElementAtOrDefault(index);

      if (pokemon == null) continue;
      entry.SetPokeName(pokemon.Pokemon.Name);
    }

    entries.First().GrabFocus();
  }

  public override void _GuiInput(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.UI_CANCEL)) {
      QueueFree();
      AcceptEvent();
    }

  }
}
