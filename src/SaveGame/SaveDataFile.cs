using System;
using Godot;

public partial class SaveDataFile : Resource {
  [Export] public SaveData[] SaveSlots = Array.Empty<SaveData>();
}
