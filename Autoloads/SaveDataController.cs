using System.Linq;
using Godot;

public interface ISaveController {
  public ISaveData SetActiveSlot(int id);
  public ISaveData GetActiveSaveData();
  ISaveData[] GetExistingSaves();
  public bool SaveGame(SaveGameArgs args);
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

  public ISaveData[] GetExistingSaves() {
    return _fileApi.SaveSlots;
  }

  public bool SaveGame(SaveGameArgs args) {
    var activeSave = GetActiveSaveData();
    activeSave.PlayerGridLocation = args.GlobalCharacterLocation;
    activeSave.ActiveMap = args.CurrentMapName;
    activeSave.IsNewGame = false;

    var saveEr = ResourceSaver.Save(_fileApi, SAVE_LOCATION);
    GD.Print("Saving Game Says:" + saveEr);
    return saveEr == Error.Ok;
  }

  public ISaveData GetActiveSaveData() {
    if (_activeSaveSlot == -1) {
      return TestSave;
    }

    return _fileApi.SaveSlots.First(x => x.SaveSlotId == _activeSaveSlot);
  }

  public ISaveData SetActiveSlot(int id) {
    _activeSaveSlot = id;
    ISaveData save = _fileApi.SaveSlots.FirstOrDefault(x => x.SaveSlotId == id);

    if (save == null) {
      save = new SaveData {
        SaveSlotId = id
      };
      if (id >= 0) { // avoid saving testing slot
        _fileApi.SaveSlots = _fileApi.SaveSlots.Append(save).Select(x => x as SaveData).ToArray();
      }
    }
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
