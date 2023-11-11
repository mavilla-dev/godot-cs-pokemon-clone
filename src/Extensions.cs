using Godot;

public static class GodotExtensions {
  public static void HideControl(this Control control) {
    control.Visible = false;
    control.ProcessMode = Node.ProcessModeEnum.Disabled;
  }

  public static void ShowControl(this Control control) {
    control.Visible = true;
    control.ProcessMode = Node.ProcessModeEnum.Inherit;
  }

  public static SaveDataController GetSaveDataManager(this Node node)
    => node.GetNode<SaveDataController>("/root/SaveDataController");

  public static PlayerGridMovementController GetPlayerGridMovementController(this Node node)
    => node.GetNode<PlayerGridMovementController>("/root/PlayerGridMovementController");

  public static ResourceDatabase GetResourceDatabase(this Node node)
    => node.GetNode<ResourceDatabase>("/root/ResourceDatabase");

  public static TileMapController GetTileMapController(this Node node)
    => node.GetNode<TileMapController>("/root/TileMapController");

  public static IPokemonController GetPokemonController(this Node node)
    => node.GetNode<PokemonController>("/root/PokemonController");
}
