using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.Persistent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;
using System.Linq;


namespace Gamedev.Main.UI.Menu.Level
{
	public partial class LevelSelect : ScrollableMenu<GameState, GameState>
	{
		[Export]
		private PackedScene PanelScene;
		[Export]
		private Label Title;

		protected override event Action<GameState> HideEvent
		{
			add
			{
				PersistentEvents.LevelSelected += value;
			}
			remove
			{
				PersistentEvents.LevelSelected -= value;
			}
		}
		
		protected override event Action<GameState> ShowEvent
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

		protected override void GenerateChildren(GameState state)
		{
			Scrollable.Instance.GetChildren().ToList().ForEach(c => c.QueueFree());
			for (int i = 0; i < state.File.CompletedLevels[state.CurrentWorld].Length; i++)
			{
				bool completed = state.File.CompletedLevels[state.CurrentWorld][i];
				LevelSelectPanel scene = PanelScene.Instantiate<LevelSelectPanel>();
				scene.State = new GameState
				{
					File = state.File,
					CurrentWorld = state.CurrentWorld,
					CurrentLevel = i,
					CurrentRoom = 0,
				};
				Scrollable.Instance.AddChild(scene);
				Title.Text = $"World {state.CurrentWorld}";
			}
		}
	}
}
