using Gamedev.Main.Events;
using Gamedev.Main.UI.Debug;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class RoomDebugBox : VBoxContainer
{
	[Export]
	private PackedScene LabelScene;
	private string[] Names = {
		"World",
		"Level",
		"Room",
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

		DebugEvents.RoomChanged += (data) =>
		{
			Labels[nameof(data.World)].DataLabel.Text = data.World.ToString();
			Labels[nameof(data.Level)].DataLabel.Text = data.Level.ToString();
			Labels[nameof(data.Room)].DataLabel.Text = data.Room.ToString();
		};
	}
}
