using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.Persistent;
using Gamedev.Main.UI.Menu;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;
using System.Linq;

public partial class OverworldMenu : ScrollableMenu<GameState, GameState>
{
	[Export]
	private PackedScene WorldScene;

	protected override event Action<GameState> HideEvent
	{
		add
		{
			PersistentEvents.WorldSelected += value;
		}
		remove
		{
			PersistentEvents.WorldSelected -= value;
		}
	}

	protected override event Action<GameState> ShowEvent
	{
		add
		{
			PersistentEvents.SaveSelected += value;
		}
		remove
		{
			PersistentEvents.SaveSelected -= value;
		}
	}

	protected override void GenerateChildren(GameState state)
	{
		Scrollable.Instance.GetChildren().ToList().ForEach(c => c.QueueFree());
		for (int i = 0; i < state.File.CompletedLevels.Length; i++)
		{
			WorldPanel panel = WorldScene.Instantiate<WorldPanel>();
			panel.UpdateChildren(new GameState
			{
				File = state.File,
				CurrentWorld = i,
				CurrentLevel = state.CurrentLevel,
				CurrentRoom = state.CurrentRoom,
			});
			Scrollable.Instance.AddChild(panel);
		}
	}
}
