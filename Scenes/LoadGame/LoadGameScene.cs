using System.Linq;
using Godot;

public partial class LoadGameScene : Control {
  [Export] public Resource LoadGameSlotScene;
  [Export] public Control GameSlotRoot;

  [Signal] public delegate void OnLoadSelectedEventHandler(int saveSlotIndex);
  [Signal] public delegate void OnCancelEventHandler();

  private int MAX_SAVES = 3;
  private int _selectionIndex = 0;
  private LoadGameSlot[] _slots;

  public override void _Ready() {
    ClearTestData();
    SetupSaveSlots();
  }

  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.ACCEPT)) {
      EmitSignal(SignalName.OnLoadSelected, _selectionIndex);
      GD.Print("OnLoad Emit");
    }

    if (ev.IsActionPressed(Constants.InputActions.CANCEL)) {
      EmitSignal(SignalName.OnCancel);
      GD.Print("OnCancel Emit");
    }

    if (ev.IsActionPressed(Constants.InputActions.UP)) {
      _slots[_selectionIndex].SetSelected(false);
      _selectionIndex = Mathf.Clamp(_selectionIndex - 1, 0, _slots.Length - 1);
      _slots[_selectionIndex].SetSelected(true);
    }

    if (ev.IsActionPressed(Constants.InputActions.DOWN)) {
      _slots[_selectionIndex].SetSelected(false);
      _selectionIndex = Mathf.Clamp(_selectionIndex + 1, 0, _slots.Length - 1);
      _slots[_selectionIndex].SetSelected(true);
    }

    AcceptEvent();
  }

  private LoadGameSlot[] InitGameSlots() {
    var packedScene = GD.Load<PackedScene>(LoadGameSlotScene.ResourcePath);

    var slots = new LoadGameSlot[MAX_SAVES];
    for (int i = 0; i < slots.Length; i++) {
      var slot = packedScene.Instantiate<LoadGameSlot>();
      slots[i] = slot;
      slot.SetNewGameView();
      GameSlotRoot.AddChild(slot);
      slot.SetSlotNumber(i + 1);
      slot.SetSelected(false);
    }
    return slots;
  }

  private void ClearTestData() {
    foreach (var child in GameSlotRoot.GetChildren()) {
      child.Free();
    }
  }

  private void SetupSaveSlots() {
    SaveData[] saves = SaveController.GetSaveSlots();
    _slots = InitGameSlots();

    for (int i = 0; i < _slots.Length; i++) {
      var save = saves.FirstOrDefault(x => x.SaveSlotId == i);
      if (save == null) continue;

      var slot = _slots[i];
      slot.SetExistingGameView();
      slot.SetTrainerName(save.TrainerName);
      // todo Add more data
    }

    _slots[_selectionIndex].SetSelected(true);
  }

  private SaveDataController SaveController => this.GetSaveDataManager();
}
