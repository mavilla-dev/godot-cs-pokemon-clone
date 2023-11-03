using System.Reflection.Metadata;
using Godot;

public partial class Pokedex : Control
{
  [Export] private PokemonDatabase PokemonDatabase { get; set; }
  [Export] private PackedScene PokedexEntry { get; set; }
  [Export] private Control EntryRoot { get; set; }
  [Export] private ScrollContainer ScrollContainer { get; set; }

  private int _selectionIndex = 0;

  public override void _Ready()
  {
    // Clear out test data
    foreach (var child in EntryRoot.GetChildren())
    {
      child.Free();
    }

    // Populate with real data
    foreach (var pokemon in PokemonDatabase.Pokemon)
    {
      var node = PokedexEntry.Instantiate<PokedexEntry>();
      EntryRoot.AddChild(node);
      node.FillEntry(pokemon.PokedexNumber, pokemon.Name, GD.Randf() > 0.5f);
    }

    // Select the first
    var first = EntryRoot.GetChildren()[_selectionIndex] as PokedexEntry;
    first.IsEntrySelected(true);

    // ScrollContainer.QueueSort();
  }

  public override void _Input(InputEvent @event)
  {
    if (@event.IsActionPressed("ui_up") && _selectionIndex > 0)
    {
      UpdateSelection(_selectionIndex - 1);
    }

    if (@event.IsActionPressed("ui_down") && _selectionIndex < EntryRoot.GetChildCount() - 1)
    {
      UpdateSelection(_selectionIndex + 1);
    }
  }

  private void UpdateSelection(int newIndex)
  {
    EntryRoot.GetChild<PokedexEntry>(_selectionIndex).IsEntrySelected(false);
    _selectionIndex = newIndex;
    var node = EntryRoot.GetChild<PokedexEntry>(_selectionIndex);
    node.IsEntrySelected(true);
    ScrollContainer.EnsureControlVisible(node);
    AcceptEvent();
  }
}
