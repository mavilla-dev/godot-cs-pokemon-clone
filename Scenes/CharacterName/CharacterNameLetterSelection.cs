using Godot;

public partial class CharacterNameLetterSelection : Button {
  [Export] private StyleBox Highlight;
  [Export] private StyleBox Empty;

  public override void _Ready() {
    SetSelected(false);
  }

  public void SetText(string text) {
    Text = text;
  }

  public void SetSelected(bool isSelected) {
    //
  }
}
