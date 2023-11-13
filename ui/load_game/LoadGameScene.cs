using System.Linq;
using Godot;

public partial class LoadGameScene : Control {
  [Export] private PackedScene _saveSlotEntry;

  [Signal] public delegate void OnSaveSelectedEventHandler(int saveSlotId);
  [Signal] public delegate void OnGoBackEventHandler();

  private Node _slotRoot;
  private int MAX_SAVES = 3;


  public override void _Ready() {
    _slotRoot = this.GetNodeOrDefault("%SlotRoot");
    ClearTestData();
    SetupSaveSlots();
  }

  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.UI_CANCEL)) {
      AcceptEvent();
      EmitSignal(SignalName.OnGoBack);
    }
  }

  private LoadGameSlot[] InitGameSlots() {
    var slots = new LoadGameSlot[MAX_SAVES];
    for (int index = 0; index < slots.Length; index++) {
      var slot = _saveSlotEntry.Instantiate<LoadGameSlot>();
      _slotRoot.AddChild(slot);
      slots[index] = slot;
      slot.SetNewGameView();
      slot.SetSlotNumber(index + 1);
      slot.SlotId = index;
      slot.Pressed += () => EmitSignal(SignalName.OnSaveSelected, slot.SlotId);
    }
    return slots;
  }

  private void ClearTestData() {
    foreach (var child in _slotRoot.GetChildren()) {
      child.Free();
    }
  }

  private void SetupSaveSlots() {
    SaveData[] saves = SaveController.GetSaveSlots();
    var slots = InitGameSlots();
    slots.First().GrabFocus();

    for (int i = 0; i < slots.Length; i++) {
      var save = saves.FirstOrDefault(x => x.SaveSlotId == i);
      if (save == null) continue;

      var slot = slots[i];
      slot.SetExistingGameView();
      slot.SetTrainerName(save.TrainerName);
      // todo Add more data
    }
  }

  private SaveDataController SaveController => this.GetSaveDataManager();
}
