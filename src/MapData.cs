using System.Linq;
using Godot;

public partial class MapData : TileMap {
  public WorldTeleport[] TeleportAreas;

  [Signal] public delegate void OnRequestTeleportEventHandler(int targetMap, int targetKey);

  public override void _Ready() {
    PopulateTeleportAreas();
    Scale = Constants.PIXEL_SCALE;
  }

  private void PopulateTeleportAreas() {
    TeleportAreas = GetChildren().Select(x => x as WorldTeleport).Where(x => x != null).ToArray();

    foreach (var area in TeleportAreas) {
      area.OnTeleportRequested += HandleTeleportRequest;
    }
  }

  private void HandleTeleportRequest(WorldTeleport tp) {
    GD.Print($"- Node Entered: {tp.Name} || [{tp.Key}] Requesting Teleport to Map '{tp.TargetMap}' / Location '{tp.TargetKey}' ");
    EmitSignal(SignalName.OnRequestTeleport, (int)tp.TargetMap, (int)tp.TargetKey);
  }
}
