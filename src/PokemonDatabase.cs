using Godot;

public partial class PokemonDatabase : Resource
{
  [Export] public Pokemon[] Pokemon { get; set; }
}
