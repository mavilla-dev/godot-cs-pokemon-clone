using Godot;

[GlobalClass]
public partial class PokemonBeltEntry : Control {
  [Export] private TextureRect SelectionArrow { get; set; }
  [Export] private TextureRect PokemonIcon { get; set; }
  [Export] private Label LabelMaxHealth { get; set; }
  [Export] private Label LabelActiveHealth { get; set; }
  [Export] private Label LabelPokemonName { get; set; }

  #region Lifecycle
  public override void _Ready() {
  }

  public override void _Draw() {
    GD.Print("Draw called on " + Name);
    SelectionArrow.ClipChildren = HasFocus()
      ? ClipChildrenMode.Disabled
      : ClipChildrenMode.Only;
  }
  #endregion Lifecycle

  public void SetPokeName(string nickname, string pokeName) {
    LabelPokemonName.Text = nickname.Trim() != string.Empty
      ? nickname.Trim()
      : pokeName;
  }
}
