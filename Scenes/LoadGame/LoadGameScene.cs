using System.Collections.Generic;
using Godot;

public partial class LoadGameScene : Control {
  [Export] public Resource LoadGameSlotScene;
  [Export] public Control GameSlotRoot;

  [Signal] public delegate void OnLoadSelectedEventHandler();
  [Signal] public delegate void OnCancelEventHandler();

  private int MAX_SAVES = 3;

  public override void _Ready() {
    // Clear Test Data
    foreach (var child in GameSlotRoot.GetChildren()) {
      child.Free();
    }

    IList<SaveData> saves = SaveController.GetSaveSlots();
    LoadGameSlot[] slots = InitGameSlots();

    for (int i = 0; i < saves.Count; i++) {
      var save = saves[i];
      var slot = slots[i];

      slot.SetExistingGameView();
      slot.SetTrainerName(save.TrainerName);
      // Add more data
    }
  }

  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.ACCEPT)) {
      // modify this to send some kind of save data information
      EmitSignal(SignalName.OnLoadSelected);
      GD.Print("OnLoad Emit");
    }

    if (ev.IsActionPressed(Constants.InputActions.CANCEL)) {
      EmitSignal(SignalName.OnCancel);
      GD.Print("OnCancel Emit");
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
    }
    return slots;
  }

  private SaveDataController SaveController => this.GetSaveDataManager();
}
