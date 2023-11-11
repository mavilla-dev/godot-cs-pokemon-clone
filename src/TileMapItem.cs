using Godot;

public abstract partial class TileMapItem : Area2D {
  [Export] public bool RemoveTile = true;
  [Export] public int TileLayer = 1; // Assumes Walls
  [Export] public string ItemKey = string.Empty;

  public override void _Ready() {
    // if ItemKey exists in master item DB then we were collected
    // so EraseCell();
  }

  public virtual void Activate() {
    if (RemoveTile) {
      EraseCell();
    }
  }

  private void EraseCell() {
    var tileMap = this.GetTileMapController().TileMap;
    tileMap.EraseCell(TileLayer, tileMap.LocalToMap(Position));
    QueueFree();
  }
}
