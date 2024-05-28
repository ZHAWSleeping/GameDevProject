using Gamedev.Main.Events;
using Gamedev.Main.UI.Debug;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamedev.Main.UI.Debug
{
	public partial class RoomDebugBox : VBoxContainer
	{
		[Export]
		private PackedScene LabelScene;
		private string[] Names = {
		"CurrentWorld",
		"CurrentLevel",
		"CurrentRoom",
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
				Labels[nameof(data.State.CurrentWorld)].DataLabel.Text = data.State.CurrentWorld.ToString();
				Labels[nameof(data.State.CurrentLevel)].DataLabel.Text = data.State.CurrentLevel.ToString();
				Labels[nameof(data.State.CurrentRoom)].DataLabel.Text = data.State.CurrentRoom.ToString();
			};
		}
	}
}