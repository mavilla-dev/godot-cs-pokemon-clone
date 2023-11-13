using Godot;

public partial class AutoloadStartup : Node {
  public override void _Ready() {
    Autoload.Instance = this;
  }
}

public static class Autoload {
  public static Node Instance;

  public static ISaveController SaveDataController
    => Instance.GetNode<SaveDataController>("/root/SaveDataController");

  public static ResourceDatabase ResourceDatabase
    => Instance.GetNode<ResourceDatabase>("/root/ResourceDatabase");

  public static ITileMapController MapController
    => Instance.GetNode<TileMapController>("/root/TileMapController");

  public static IPokemonController PokemonController
    => Instance.GetNode<PokemonController>("/root/PokemonController");
}
