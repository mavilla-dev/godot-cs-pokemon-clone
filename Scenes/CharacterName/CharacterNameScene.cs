using System.Collections.Generic;
using Godot;

public partial class CharacterNameScene : Control {
  [Export] private PackedScene CharacterNameLabel;

  private Node _specialButtonRoot;
  private Label _name;
  private GridContainer _gridContainer;

  public override void _Ready() {
    _specialButtonRoot = GetNode("%ButtonRoot");
    _name = GetNode<Label>("%Name");
    _gridContainer = GetNode<GridContainer>("%LetterGrid");

    ClearTest();
    SetupLetters();
    SetupSpecialButtons();
    var firstButton = _gridContainer.GetChild(0) as Control;
    firstButton.GrabFocus();
  }

  private void ClearTest() {
    foreach (var child in _gridContainer.GetChildren()) {
      child.Free();
    }

    _name.Text = string.Empty;
  }

  private void SetupLetters() {
    foreach (var letter in Letters) {
      var button = CharacterNameLabel.Instantiate<CharacterNameLetterSelection>();
      _gridContainer.AddChild(button);
      button.SetText(letter);
      button.SetSelected(false);
      button.ButtonUp += () => { _name.Text += letter; };
    }
  }

  private void SetupSpecialButtons() {
    // Add DEL button
    var delButton = CharacterNameLabel.Instantiate<CharacterNameLetterSelection>();
    _specialButtonRoot.AddChild(delButton);

    delButton.SetText("DEL");
    delButton.SetSelected(false);
    delButton.Pressed += () => {
      if (_name.Text.Length == 0) return;

      _name.Text = _name.Text.Substr(0, _name.Text.Length - 1);
    };

    // Add OK Button
    var okButton = CharacterNameLabel.Instantiate<CharacterNameLetterSelection>();
    _specialButtonRoot.AddChild(okButton);

    okButton.SetText("OK");
    okButton.SetSelected(false);
    okButton.Pressed += () => {
      if (_name.Text.Length == 0) return;

      var saveData = Autoload.SaveDataController.GetActiveSaveData();
      saveData.TrainerName = _name.Text.Trim();
      QueueFree();
      Autoload.MapController.LoadStartingZone();
    };
  }

  private static readonly List<string> Letters = new() {
    "A",
    "B",
    "C",
    "D",
    "E",
    "F",
    "G",
    "H",
    "I",
    "J",
    "K",
    "L",
    "M",
    "N",
    "O",
    "P",
    "Q",
    "R",
    "S",
    "T",
    "U",
    "V",
    "X",
    "Y",
    "Z",
  };
}
