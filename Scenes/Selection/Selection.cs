using Godot;

public partial class Selection : Control {
  [Export] public int FontSize = 80;
  [Export] public string LabelText = "Item 1";

  [Export] private Label SelectionName;
  [Export] private TextureRect TextureRect;
  [Export] private Color SelectionIconColor;

  private static Color Transparent = new(0, 0, 0, 0);

  public override void _Ready() {
    SelectionName.Text = LabelText;
    SelectionName.AddThemeFontSizeOverride("font_size", FontSize);
  }

  public void IsSelected(bool isSelected) {
    TextureRect.Modulate = isSelected
      ? SelectionIconColor
      : Transparent;
  }
}
