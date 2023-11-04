using Godot;

[GlobalClass]
public partial class Pokemon : Resource
{
  [Export] public int PokedexNumber { get; set; }
  [Export] public string Name { get; set; }
  [Export] public int HP { get; set; }
  [Export] public int Atk { get; set; }
  [Export] public int Def { get; set; }
  [Export] public int SpAtk { get; set; }
  [Export] public int SpDef { get; set; }
  [Export] public int Speed { get; set; }
  [Export] public AtlasTexture PokedexImage { get; set; }
}
