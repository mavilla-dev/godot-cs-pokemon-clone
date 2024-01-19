using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class MessageBox : CanvasLayer {
  [Export(PropertyHint.Range, "0,5,")] public double TextSpeedNormal = 1;

  [Signal] public delegate void OnDoneEventHandler();

  private Label _messageBox;
  private Timer _timer;
  private TextureRect _arrow;
  private ScrollContainer _scroll;

  private Queue<string> _messageQueue = new();
  private Queue<char> _activeMessage = new();
  private int _skipCount = 0;
  private bool _waitForNext = false;
  private bool _staggerText = false;
  private State _state = State.Idle;
  private double _timePassed = 0;

  private enum State {
    Idle,
    Printing,
    PrintingDone,
  }

  public override void _Ready() {
    Hide();
    _messageBox = GetNode<Label>("%Label");
    _timer = GetNode<Timer>("%Timer");
    _arrow = GetNode<TextureRect>("%Arrow");
    _scroll = GetNode<ScrollContainer>("%ScrollContainer");

    // _timer.Timeout += OnTimerDisplayLetter;
    _timer.OneShot = false;

    SetText(new string[] {
      "Hello there! Welcome to the world of Pokémon! My name is Oak! People call me the Pokémon Prof! Hello there! Welcome to the world of Pokémon! My name is Oak! People call me the Pokémon Prof!",
      "Lorem Ipsum",
    });
    Play();
    _scroll.GetVScrollBar().Changed += () => _scroll.EnsureControlVisible(_messageBox);
  }

  public override void _Process(double delta) {
    if (_state != State.Printing) return;

    _timePassed += delta;

    if (_timePassed < TextSpeedNormal) return;
    _timePassed = 0;

    if (_activeMessage.Count == 0) {
      SwitchToPrintDoneState();
      return;
    }

    if (_skipCount >= 2) {
      _skipCount = 0;
      SwitchToPrintDoneState();
      foreach (var letter in _activeMessage) {
        _messageBox.Text += letter;
      }
      return;
    }

    _messageBox.Text += _activeMessage.Dequeue();
  }

  public override void _Input(InputEvent ev) {
    switch (_state) {
      case State.Idle:
        // do nothing
        break;

      case State.Printing:
        // allow speed up and skip
        if (ev.IsActionPressed(Constants.InputActions.ACCEPT) || ev.IsActionPressed(Constants.InputActions.CANCEL)) {
          if (_skipCount >= 2) {
            return;
          }
          _skipCount++;
        }
        break;

      case State.PrintingDone:
        if (ev.IsActionPressed(Constants.InputActions.ACCEPT) || ev.IsActionPressed(Constants.InputActions.CANCEL)) {
          // if we still have messages, show them
          if (_messageQueue.Count > 0) {
            SwitchToPrintingState();
            return;
          }

          // No messages left, signal and go idle
          _state = State.Idle;
          EmitSignal(SignalName.OnDone);
        }
        break;
    }
  }

  private void OnTimerDisplayLetter() {
    if (_activeMessage.Count == 0) {
      SwitchToPrintDoneState();
      return;
    }

    if (_skipCount >= 2) {
      _skipCount = 0;
      SwitchToPrintDoneState();
      foreach (var letter in _activeMessage) {
        _messageBox.Text += letter;
      }
      return;
    }

    _messageBox.Text += _activeMessage.Dequeue();
  }

  public void SetText(string[] text) {
    _messageQueue = new Queue<string>(text);
  }

  public void Play(bool staggerText = true) {
    GD.Print("Play began with Messages: " + _messageQueue.Count);
    Show();
    _staggerText = staggerText;
    _timer.WaitTime = TextSpeedNormal;
    _messageBox.Text = string.Empty;
    _messageBox.GrabFocus();

    if (_messageQueue.Count == 0) {
      GD.PrintErr("Cannot play messages with empty queue");
      _arrow.Show();
      EmitSignal(SignalName.OnDone);
      _state = State.Idle;
    };

    SwitchToPrintingState();
  }

  private bool TryDequeueNextMessage() {
    var success = _messageQueue.TryDequeue(out string nextMsg);
    _activeMessage = new Queue<char>(nextMsg?.ToCharArray());
    return success;
  }

  private void SwitchToPrintingState() {
    _messageBox.Text = string.Empty;
    _timer.Start();
    _arrow.Hide();
    _state = State.Printing;
    TryDequeueNextMessage();
  }

  private void SwitchToPrintDoneState() {
    _arrow.Show();
    _state = State.PrintingDone;
    _timer.Stop();
  }
}
