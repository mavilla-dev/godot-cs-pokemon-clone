using Godot;

public partial class CaughtPokemon : Resource {
  public Pokemon Pokemon { get; set; }
  public int Level { get; set; }
  public int XP { get; set; }
  public string Nickname { get; set; }
}
