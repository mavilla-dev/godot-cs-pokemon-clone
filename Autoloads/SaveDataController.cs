using System.Linq;
using Godot;

public partial class SaveDataController : Node {
  private const string SAVE_LOCATION = "user://savegame.res";
  [Export] private SaveDataFile _saveFile = new();

  public int SelectedSaveSlotIndex { get; set; } = -1;

  public override void _Ready() {
    SetupSaveData();
  }

  public SaveData[] GetSaveSlots() {
    return _saveFile.SaveSlots;
  }

  public void SavePlayerName(string trainerName) {
    GetActiveSave().TrainerName = trainerName;
  }

  public bool SaveGame() {
    var activeSave = GetActiveSave();
    activeSave.PlayerGridLocation = this.GetPlayerGridMovementController().GetGlobalPlayerGridLocation();
    // activeSave.ActiveMap = this.

    var saveEr = ResourceSaver.Save(_saveFile, SAVE_LOCATION);
    GD.Print("Saving Game Says:" + saveEr);
    return saveEr == Error.Ok;
  }

  internal void PopulateSlot(int saveSlotIndex) {
    SelectedSaveSlotIndex = saveSlotIndex;
    _saveFile.SaveSlots = _saveFile.SaveSlots.Append(
      new SaveData {
        SaveSlotId = saveSlotIndex
      }).ToArray();
  }

  #region Private

  private SaveData TestSave = new();

  private SaveData GetActiveSave() {
    if (SelectedSaveSlotIndex == -1) {
      return TestSave;
    }

    return _saveFile.SaveSlots.First(x => x.SaveSlotId == SelectedSaveSlotIndex);
  }

  private SaveDataFile LoadGame() {
    return ResourceLoader.Load<SaveDataFile>(SAVE_LOCATION);
  }

  private void SetupSaveData() {
    bool exist = DoesSaveFileExist();
    if (exist) {
      _saveFile = LoadGame();
      return;
    }
  }

  private bool DoesSaveFileExist() {
    return FileAccess.FileExists(SAVE_LOCATION);
  }

  #endregion Private
}
