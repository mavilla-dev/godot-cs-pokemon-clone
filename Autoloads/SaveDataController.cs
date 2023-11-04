using System.Collections;
using System.Collections.Generic;
using Godot;

public partial class SaveDataController : Node {
  private const string SAVE_LOCATION = "user://savegame.dat";
  private SaveDataFile _saveFile = new();

  public override void _Ready() {
    SetupSaveData();
  }

  private void SetupSaveData() {
    bool exist = DoesSaveFileExist();
    if (exist) {
      _saveFile = LoadSaveFile();
      return;
    }
  }

  private void SaveData() {
    ResourceSaver.Save(_saveFile, SAVE_LOCATION);
  }

  private SaveDataFile LoadSaveFile() {
    return ResourceLoader.Load<SaveDataFile>(SAVE_LOCATION);
  }

  private bool DoesSaveFileExist() {
    return FileAccess.FileExists(SAVE_LOCATION);
  }

  public IList<SaveData> GetSaveSlots() {
    return _saveFile.SaveSlots;
  }
}
