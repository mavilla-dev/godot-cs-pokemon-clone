using System.Collections.Generic;
using System.Linq;
using Godot;

public enum StartMenuOption {
  Pokemon,
  Save,
  Items,
  Stats,
}

public partial class StartMenu : Control {
  [Signal] public delegate void OnCloseEventHandler();
  [Signal] public delegate void OnOptionSelectedEventHandler(int startMenuOption);

  [Export] private Resource SelectItemResource;
  [Export] private Node SelectionRoot;

  [ExportCategory("Sub-scenes")]
  [Export] public Resource PokemonResource;

  private List<Selection> _options = new();
  private int _index;

  private static Dictionary<string, StartMenuOption> _optionDict = new() {
    { "Pokemon", StartMenuOption.Pokemon},
    { "Items", StartMenuOption.Items},
    { "Stats", StartMenuOption.Stats},
    { "Save", StartMenuOption.Save},
  };

  public override void _Ready() {
    ClearTestOptions();
    PopulateOptions();
  }

  private void PopulateOptions() {
    var selectItemScene = GD.Load<PackedScene>(SelectItemResource.ResourcePath);

    foreach (var option in _optionDict) {
      var selection = selectItemScene.Instantiate<Selection>();
      selection.LabelText = option.Key;
      selection.IsSelected(false);

      SelectionRoot.AddChild(selection);
      _options.Add(selection);
    }

    _options[0].IsSelected(true);
  }

  private void ClearTestOptions() {
    foreach (var child in SelectionRoot.GetChildren()) {
      child.Free();
    }
  }

  public override void _Input(InputEvent ev) {
    _options[_index].IsSelected(false);
    if (ev.IsActionPressed(Constants.InputActions.DOWN)) {
      _index = Mathf.Clamp(_index + 1, 0, _options.Count - 1);
    }

    if (ev.IsActionPressed(Constants.InputActions.UP)) {
      _index = Mathf.Clamp(_index - 1, 0, _options.Count - 1);
    }

    if (ev.IsActionPressed(Constants.InputActions.CANCEL)
      || ev.IsActionPressed(Constants.InputActions.START)) {
      GD.Print("Emitting close");
      EmitSignal(SignalName.OnClose);
      _index = 0;
    }

    if (ev.IsActionPressed(Constants.InputActions.ACCEPT)) {
      var option = _optionDict.ElementAt(_index).Value;
      GD.Print("Emitting option " + option);
      EmitSignal(SignalName.OnOptionSelected, (int)option);
    }

    _options[_index].IsSelected(true);
    AcceptEvent();
  }
}
