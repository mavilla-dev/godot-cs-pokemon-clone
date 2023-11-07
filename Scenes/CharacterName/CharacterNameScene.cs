using System.Collections.Generic;
using Godot;

public partial class CharacterNameScene : Control {
  [Export] private PackedScene CharacterNameLabel;
  [Export] private GridContainer GridContainer;
  [Export] private Label NameLabel;
  [Export] private Node SpecialButtons;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {
    ClearTest();
    SetupLetters();
    SetupSpecialButtons();
    var firstButton = GridContainer.GetChild(0) as Control;
    firstButton.GrabFocus();
  }

  private void ClearTest() {
    foreach (var child in GridContainer.GetChildren()) {
      child.Free();
    }

    NameLabel.Text = string.Empty;
  }

  private void SetupLetters() {
    foreach (var letter in Letters) {
      var button = CharacterNameLabel.Instantiate<CharacterNameLetterSelection>();
      GridContainer.AddChild(button);
      button.SetText(letter);
      button.SetSelected(false);
      button.Pressed += () => { NameLabel.Text += letter; };
    }
  }

  private void SetupSpecialButtons() {
    // Add DEL button
    var delButton = CharacterNameLabel.Instantiate<CharacterNameLetterSelection>();
    SpecialButtons.AddChild(delButton);

    delButton.SetText("DEL");
    delButton.SetSelected(false);
    delButton.Pressed += () => {
      if (NameLabel.Text.Length == 0) return;

      NameLabel.Text = NameLabel.Text.Substr(0, NameLabel.Text.Length - 1);
    };

    // Add OK Button
    var okButton = CharacterNameLabel.Instantiate<CharacterNameLetterSelection>();
    SpecialButtons.AddChild(okButton);

    okButton.SetText("OK");
    okButton.SetSelected(false);
    okButton.Pressed += () => {
      if (NameLabel.Text.Length == 0) return;

      SaveController.SavePlayerName(NameLabel.Text);
      QueueFree();
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

  private SaveDataController SaveController => this.GetSaveDataManager();
}
