using Godot;

public partial class SaveData : Resource {
  [Export] public int SaveSlotId;
  [Export] public string TrainerName;
  [Export] public Vector2I PlayerGridLocation;
  [Export] public MapName ActiveMap;
}
