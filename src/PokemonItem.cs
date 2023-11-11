using Godot;

[GlobalClass]
public partial class PokemonItem : TileMapItem {
  [Export] public Pokemon Pokemon { get; set; }
  [Export] public int Level { get; set; }
  [Export] public int XP { get; set; }

  public override void Activate() {
    var pokeController = this.GetPokemonController();
    pokeController.PokemonBelt.Add(new CaughtPokemon() {
      Pokemon = Pokemon,
      Level = Level,
      XP = XP
    });

    base.Activate();
  }
}
