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

  public static Node GetNodeOrDefault(this Node n, string pathName) {
    Node node = n.GetNode(pathName);
    if (node == null) {
      GD.PrintErr($"Node NOT found, {pathName}");
    }
    return node;
  }

  public static T GetNodeOrDefault<T>(this Node n, string pathName) where T : Node {
    T node = n.GetNode<T>(pathName);
    if (node == null) {
      GD.PrintErr($"Node NOT found, {pathName}");
    }
    return node;
  }

  public static SaveDataController GetSaveDataManager(this Node node)
    => node.GetNode<SaveDataController>("/root/SaveDataController");

  public static ResourceDatabase GetResourceDatabase(this Node node)
    => node.GetNode<ResourceDatabase>("/root/ResourceDatabase");

  public static TileMapController GetTileMapController(this Node node)
    => node.GetNode<TileMapController>("/root/TileMapController");

  public static IPokemonController GetPokemonController(this Node node)
    => node.GetNode<PokemonController>("/root/PokemonController");
}
