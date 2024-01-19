using System;
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
      Autoload.SceneChanger.ChangeScene(Autoload.SceneChanger.StartScreen);
    }
  }

  private void HandleSaveSelected(LoadGameSlot loadGameSlot) {
    GD.Print("SLOT " + loadGameSlot.SlotId);
    var saveData = Autoload.SaveDataController.SetActiveSlot(loadGameSlot.SlotId);
    if (saveData.IsNewGame) {
      Autoload.SceneChanger.ChangeScene(Autoload.SceneChanger.NameInputScene);
    } else {
      QueueFree();
      Autoload.MapController.LoadSavedGame(saveData);
    }
  }

  private void ClearTestData() {
    foreach (var child in _slotRoot.GetChildren()) {
      child.Free();
    }
  }

  private void SetupSaveSlots() {
    var existingSaves = Autoload.SaveDataController.GetExistingSaves();
    var slots = new LoadGameSlot[MAX_SAVES];

    for (int index = 0; index < slots.Length; index++) {
      var save = existingSaves.FirstOrDefault(x => x.SaveSlotId == index);
      var slot = _saveSlotEntry.Instantiate<LoadGameSlot>();
      _slotRoot.AddChild(slot); // must AddChild first to run _Ready method

      slot.Pressed += () => HandleSaveSelected(slot);
      slots[index] = slot;
      slot.SetNewGameView();
      slot.SetSlotId(index);
      slot.Name = "Load Slot " + index;

      if (save == null) continue;
      slot.SetExistingGameView();
      slot.SetTrainerName(save.TrainerName);
    }

    slots.First().GrabFocus();
  }
}
