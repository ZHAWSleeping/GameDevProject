using Gamedev.Main.Events;
using Gamedev.Main.UI.Debug;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerDebug : VBoxContainer
{
	[Export]
	private PackedScene LabelScene;
	private string[] Names = {
		"State",
		"PreviousState",
		"Velocity",
		"InputDirection",
		"JumpHeld",
		"JumpJustPressed",
		"CoyoteTime",
		"JumpTime",
	};

	private Dictionary<string, DebugLabel> Labels;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Labels = Names.Zip(Names.Select(name =>
		{
			DebugLabel label = LabelScene.Instantiate<DebugLabel>();
			label.LabelName = name;
			AddChild(label);
			return label;
		}), (k, v) => new { k, v })
		.ToDictionary(x => x.k, x => x.v);

		DebugEvents.PlayerDataEvent += (data) =>
		{
			Labels[nameof(data.State)].DataLabel.Text = data.State.ToString();
			Labels[nameof(data.PreviousState)].DataLabel.Text = data.PreviousState.ToString();
			Labels[nameof(data.Velocity)].DataLabel.Text = data.Velocity.ToString();
			Labels[nameof(data.InputDirection)].DataLabel.Text = data.InputDirection.ToString();
			Labels[nameof(data.JumpHeld)].DataLabel.Text = data.JumpHeld.ToString();
			Labels[nameof(data.JumpJustPressed)].DataLabel.Text = data.JumpJustPressed.ToString();
			Labels[nameof(data.CoyoteTime)].DataLabel.Text = data.CoyoteTime.ToString();
			Labels[nameof(data.JumpTime)].DataLabel.Text = data.JumpTime.ToString();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
