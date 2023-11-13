using Godot;

public static class Constants {
  public static Vector2 PIXEL_SCALE = new(5, 5);

  public static class TileMapLayer {
    public const int WALKABLE = 0;
    public const int WALLS = 1;
  }

  public static class InputActions {
    public const string LEFT = "left";
    public const string RIGHT = "right";
    public const string UP = "up";
    public const string DOWN = "down";
    public const string ACCEPT = "accept";
    public const string CANCEL = "cancel";
    public const string START = "start";
    public const string SELECt = "select";

    public const string DEBUG_SPAWN = "debug_spawn";
    public const string LEFT_CLICK = "left_click";

    public const string UI_CANCEL = "ui_cancel";
    public const string UI_ACCEPT = "ui_accept";

    public const string PRINT_TREE = "debug_print_tree";
  }
}
