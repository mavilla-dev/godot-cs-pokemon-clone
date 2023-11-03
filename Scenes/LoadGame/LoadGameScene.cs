using Godot;

public partial class LoadGameScene : Control
{
	[Signal] public delegate void OnLoadSelectedEventHandler();
	[Signal] public delegate void OnCancelEventHandler();

	public override void _Input(InputEvent ev)
	{
		if (ev.IsActionPressed(Constants.InputActions.ACCEPT))
		{
			// modify this to send some kind of save data information
			EmitSignal(SignalName.OnLoadSelected);
			GD.Print("OnLoad Emit");
		}

		if (ev.IsActionPressed(Constants.InputActions.CANCEL))
		{
			EmitSignal(SignalName.OnCancel);
			GD.Print("OnCancel Emit");
		}

		AcceptEvent();
	}
}
