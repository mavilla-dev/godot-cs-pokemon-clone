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
}
