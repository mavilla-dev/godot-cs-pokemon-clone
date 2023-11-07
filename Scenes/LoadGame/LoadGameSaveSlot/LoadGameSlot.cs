using Godot;

public partial class LoadGameSlot : Control {
  [Export] private Label SlotNumber;
  [Export] private Label TrainerName;
  [Export] private Label TimePlayed;
  [Export] private Label BadgeCount;
  [Export] private Label PokedexCount;
  [Export] private Label CaughtCount;

  [Export] private Control ExistingDataRoot;
  [Export] private Control NewGameRoot;

  [Export] private TextureRect SelectionArrow;
  [Export] private Color White;
  [Export] private Color Transparent;

  public void SetSelected(bool isSelected) {
    SelectionArrow.Modulate = isSelected
      ? White
      : Transparent;
  }

  public void SetNewGameView() {
    ExistingDataRoot.HideControl();
    NewGameRoot.ShowControl();
  }

  public void SetExistingGameView() {
    ExistingDataRoot.ShowControl();
    NewGameRoot.HideControl();
  }

  public void SetSlotNumber(int num) {
    SlotNumber.Text = "Slot " + num;
  }

  public void SetTrainerName(string name) {
    TrainerName.Text = name;
  }

  public void SetTimePlayed() {
    TimePlayed.Text = "Time Played 00:00";
  }

  public void SetBadgeCount(int count) {
    BadgeCount.Text = "Badges: " + count;
  }

  public void SetPokedexCount(int count) {
    PokedexCount.Text = "Pokdex: " + count;
  }

  public void SetCaughtCount(int count) {
    PokedexCount.Text = "Caught: " + count;
  }
}
