using Godot;

public partial class WorldTeleport : Area2D {
  [ExportGroup("Local Map Data")]
  [Export] public TeleportKey Key { get; set; }
  [Export] public Node2D SpawnLocation { get; set; }

  [ExportGroup("Target Map Data")]
  [Export] public MapName TargetMap { get; set; } // What scene should be loaded
  [Export] public TeleportKey TargetKey { get; set; } // Where should player spawn

  [Signal] public delegate void OnTeleportRequestedEventHandler(WorldTeleport worldTeleport);

  public override void _Ready() {
    AreaEntered += TriggerTeleport;
  }

  private void TriggerTeleport(Area2D area) {
    if (TargetKey == TeleportKey.None || TargetMap == MapName.None) {
      GD.PrintErr("No target key or map was set. " + Key);
      return;
    }

    Autoload.MapController.SwapTilemapAsync(TargetMap, TargetKey);
  }
}

// DO NOT change keys
public enum MapName {
  None = 0,
  PalletTown_MyHouse_UpperLevel = 1,
  PalletTown_MyHouse_LowerLevel = 2,
  Overworld = 3,
}

// DO NOT change keys
public enum TeleportKey {
  None = 0,
  PalletTown_MyHouse_InitialSpawn = 1,
  PalletTown_MyHouse_UpperLevel = 2,
  PalletTown_MyHouse_LowerLevel_North = 3,
  PalletTown_MyHouse_LowerLevel_South = 4,
  PalletTown_Outside_MyHouse = 5,
}
