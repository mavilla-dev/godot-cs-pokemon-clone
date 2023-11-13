using System.Linq;
using Godot;

public interface ISaveController {
  public SaveData SetActiveSlot(int id);
}

public class SaveGameArgs {
  public Vector2I GlobalCharacterLocation;
  public MapName CurrentMapName;
}

public partial class SaveDataController : Node, ISaveController {
  private const string SAVE_LOCATION = "user://savegame.res";
  [Export] private SaveDataFile _fileApi = new();

  private int _activeSaveSlot = -1;

  #region Lifecycle
  public override void _Ready() {
    LoadSaveFile();
  }
  #endregion Lifecycle

  public SaveData[] GetSaveSlots() {
    return _fileApi.SaveSlots;
  }

  public void SavePlayerName(string trainerName) {
    GetActiveSave().TrainerName = trainerName;
  }

  public bool SaveGame(SaveGameArgs args) {
    var activeSave = GetActiveSave();
    activeSave.PlayerGridLocation = args.GlobalCharacterLocation;
    activeSave.ActiveMap = args.CurrentMapName;

    var saveEr = ResourceSaver.Save(_fileApi, SAVE_LOCATION);
    GD.Print("Saving Game Says:" + saveEr);
    return saveEr == Error.Ok;
  }

  public SaveData GetActiveSave() {
    if (_activeSaveSlot == -1) {
      return TestSave;
    }

    return _fileApi.SaveSlots.First(x => x.SaveSlotId == _activeSaveSlot);
  }

  public SaveData SetActiveSlot(int id) {
    _activeSaveSlot = id;
    SaveData save = _fileApi.SaveSlots.FirstOrDefault(x => x.SaveSlotId == id) ?? new SaveData {
      SaveSlotId = id
    };
    return save;
  }

  #region Private

  private SaveData TestSave = new();

  private SaveDataFile LoadFile() {
    return ResourceLoader.Load<SaveDataFile>(SAVE_LOCATION);
  }

  private void LoadSaveFile() {
    bool exist = DoesSaveFileExist();
    if (exist) {
      _fileApi = LoadFile();
      return;
    }
  }

  private bool DoesSaveFileExist() {
    return FileAccess.FileExists(SAVE_LOCATION);
  }



  #endregion Private
}
