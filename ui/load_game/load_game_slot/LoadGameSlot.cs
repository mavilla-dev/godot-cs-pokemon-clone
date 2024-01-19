using Godot;

public partial class LoadGameSlot : Button {
  // [Signal] public delegate void PressedEventHandler(LoadGameSlot loadGameSlot);

  private Label _slotNumber;
  private Label _trainerName;
  private Label _timePlayed;
  private Label _badgeCount;
  private Label _pokedexCount;
  private Label _caughtCount;

  private Control _existingDataRoot;
  private Control _newGameRoot;

  private TextureRect _selectArrow;
  private Color White;
  private Color Transparent;

  [Export] public int SlotId { get; private set; }

  #region Lifecycle
  public override void _Ready() {
    GetNodes();
  }

  public override void _Draw() {
    _selectArrow.ClipChildren = HasFocus()
      ? ClipChildrenMode.Disabled
      : ClipChildrenMode.Only;
  }

  // public override void _Input(InputEvent ev) {
  //   if (ev.IsActionPressed(Constants.InputActions.UI_ACCEPT)) {
  //     EmitSignal(SignalName.Pressed, this);
  //     AcceptEvent();
  //   }
  // }
  #endregion Lifecycle

  #region API

  public void SetNewGameView() {
    _existingDataRoot.Hide();
    _newGameRoot.Show();
  }

  public void SetExistingGameView() {
    _existingDataRoot.Show();
    _newGameRoot.Hide();
  }

  public void SetSlotId(int slotId) {
    SlotId = slotId;
    _slotNumber.Text = "Slot " + (slotId + 1);
  }

  public void SetTrainerName(string name) {
    _trainerName.Text = name;
  }

  public void SetTimePlayed() {
    _timePlayed.Text = "Time Played 00:00";
  }

  public void SetBadgeCount(int count) {
    _badgeCount.Text = "Badges: " + count;
  }

  public void SetPokedexCount(int count) {
    _pokedexCount.Text = "Pokdex: " + count;
  }

  public void SetCaughtCount(int count) {
    _pokedexCount.Text = "Caught: " + count;
  }

  #endregion API

  #region Privates

  private void GetNodes() {
    _slotNumber = this.GetNodeOrDefault<Label>("%Slot Number");
    _trainerName = this.GetNodeOrDefault<Label>("%Trainer Name");
    _timePlayed = this.GetNodeOrDefault<Label>("%Game Time");
    _badgeCount = this.GetNodeOrDefault<Label>("%Badges");
    _pokedexCount = this.GetNodeOrDefault<Label>("%Pokedex Count");
    _caughtCount = this.GetNodeOrDefault<Label>("%Caught");

    _existingDataRoot = this.GetNodeOrDefault<Control>("%Existing Data Root");
    _newGameRoot = this.GetNodeOrDefault<Control>("%New Game Root");

    _selectArrow = this.GetNodeOrDefault<TextureRect>("%Arrow");

  }

  #endregion Privates
}
