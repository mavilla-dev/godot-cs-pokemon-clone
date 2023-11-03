using Godot;

public partial class PokedexEntry : Control
{
  [Export] private Label PokemonNumber { get; set; }
  [Export] private Label PokemonName { get; set; }
  [Export] private TextureRect CapturedImg { get; set; }
  [Export] private TextureRect SelectionImg { get; set; }

  public void FillEntry(int dexNumber, string pokemonName, bool isCaptured)
  {
    PokemonNumber.Text = dexNumber.ToString("D3");
    PokemonName.Text = pokemonName;
    SelectionImg.Hide();

    CapturedImg.Visible = isCaptured;
  }

  public void IsEntrySelected(bool selected)
  {
    SelectionImg.Visible = selected;
  }
}
