using Godot;

public interface ISaveData {
  public int SaveSlotId { get; set; }
  public bool IsNewGame { get; set; }
  public string TrainerName { get; set; }
  public Vector2I PlayerGridLocation { get; set; }
  public MapName ActiveMap { get; set; }
}

public partial class SaveData : Resource, ISaveData {
  [Export] public int SaveSlotId { get; set; }
  [Export] public bool IsNewGame { get; set; } = true;
  [Export] public string TrainerName { get; set; }
  [Export] public Vector2I PlayerGridLocation { get; set; }
  [Export] public MapName ActiveMap { get; set; }
}
