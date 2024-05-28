using System.Collections.Generic;
using System.Linq;
using Gamedev.Main.Characters.Players;
using Gamedev.Main.Events;
using Gamedev.Main.Levels;
using Godot;

namespace Gamedev.Main.UI.Debug
{
	public partial class PlaytimeDebugBox : VBoxContainer
	{
		[Export]
		private PackedScene LabelScene;

		private string[] Names = {
			"Ticks",
			"Playtime",
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

			PersistentEvents.PlaytimeTicked += () =>
			{
				Labels["Ticks"].DataLabel.Text = LevelManager.Instance.State.File.PlaytimeTicks.ToString();
				Labels["Playtime"].DataLabel.Text = TimeTracker.DisplayTicks(LevelManager.Instance.State.File.PlaytimeTicks);
			};
		}
	}
}