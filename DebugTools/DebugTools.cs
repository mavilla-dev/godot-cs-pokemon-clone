using Godot;

public partial class DebugTools : Node2D {
  [Export] public PackedScene Character;

  private bool _spawningPlayer = false;

  public override void _Ready() {
    // MoveController.ActiveTileMap = GetTree().Root.GetNode<TileMap>("*/TileMap");
    // GD.Print(MoveController.ActiveTileMap == null ? "DEBUG TOOL: No tilemap found" : "DEBUG TOOL: Found Tilemap");
  }

  private void PrintCurrentTree() {
    PrintTree(GetTree().Root.GetChildren());
  }

  private void PrintTree(Godot.Collections.Array<Node> nodes) {
    foreach (var node in nodes) {
      if (node.GetChildCount() > 0) {
        GD.Print($"{node.Name}: Children: {node.GetChildCount()}");
        PrintTree(node.GetChildren());
      } else {
        GD.Print(node.Name);
      }
    }
  }

  public override void _Input(InputEvent ev) {
    if (ev.IsActionPressed(Constants.InputActions.PRINT_TREE)) {
      PrintCurrentTree();
      return;
    }

    if (ev.IsActionPressed(Constants.InputActions.DEBUG_SPAWN)) {
      GD.Print("Spawn Player Option Enabled");
      _spawningPlayer = true;
      return;
    }

    if (ev.IsActionPressed(Constants.InputActions.LEFT_CLICK) && _spawningPlayer) {
      GD.Print("Spawning Player");
      _spawningPlayer = false;

      if (MoveController.ActiveTileMap == null) {
        GD.PrintErr("Could not find a tilemap");
        return;
      }

      var mouseGridPosition = MoveController.ActiveTileMap.LocalToMap(ToLocal(GetGlobalMousePosition()));
      var charNode = Character.Instantiate<Character>();
      charNode.Position = MoveController.ActiveTileMap.MapToLocal(mouseGridPosition);
      // setup event listeners
      GetTree().Root.AddChild(charNode);

      MoveController.ActiveTileMap = MoveController.ActiveTileMap;
      MoveController.Character = charNode;
      return;
    }

    if ((ev.IsActionPressed(Constants.InputActions.UI_CANCEL) || ev.IsActionPressed(Constants.InputActions.CANCEL)) && _spawningPlayer) {
      _spawningPlayer = false;
      return;
    }
  }

  private void SetupOnRequestMove(int direction) {

  }

  private PlayerGridMovementController MoveController => GetNode<PlayerGridMovementController>("/root/PlayerGridMovementController");
}
