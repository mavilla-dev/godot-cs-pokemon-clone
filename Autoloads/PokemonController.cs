using System.Collections.Generic;
using Godot;

public interface IPokemonController {
  public IList<CaughtPokemon> PokemonBelt { get; set; }
}

public partial class PokemonController : Node, IPokemonController {
  private IList<CaughtPokemon> _pokemonBelt = new List<CaughtPokemon>(6);

  public IList<CaughtPokemon> PokemonBelt {
    get { return _pokemonBelt; }
    set { _pokemonBelt = value; }
  }

  // pokemon boxes
}
