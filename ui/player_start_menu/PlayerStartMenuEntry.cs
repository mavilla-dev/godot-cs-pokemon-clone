using Godot;

[Tool]
public partial class PlayerStartMenuEntry : Control {
  [Export] public string EntryText { get; set; }

  [Signal] public delegate void PressedEventHandler();

  private Label _entryName;
  private TextureRect _selectArrow;

  #region Lifecycle

  public override void _Ready() {
    _entryName = GetEntryName();

    _selectArrow = GetNode<TextureRect>(
      GetMeta("SelectArrow").As<NodePath>()
    );

    _entryName.Text = EntryText;
  }

  public override void _Process(double delta) {
    if (Engine.IsEditorHint()) {
      _entryName ??= GetEntryName();
      _entryName.Text = EntryText;
    }
  }

  public override void _Draw() {
    _selectArrow.ClipChildren = HasFocus()
      ? ClipChildrenMode.Disabled
      : ClipChildrenMode.Only;
  }

  public override void _GuiInput(InputEvent ev) {
    if (ev.IsActionPressed("ui_accept")) {
      EmitSignal(SignalName.Pressed);
      AcceptEvent();
    }

    base._GuiInput(ev);
  }

  #endregion Lifecycle

  private Label GetEntryName() => GetNode<Label>(
      GetMeta("EntryName").As<NodePath>()
    );
}
