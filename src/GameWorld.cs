using Godot;
using System;

public partial class GameWorld : SubViewport {
  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {
    Vector2I viewportSize = GetParent().GetWindow().Size;
    var scaleFactor = viewportSize / this.Size;

  }
}
