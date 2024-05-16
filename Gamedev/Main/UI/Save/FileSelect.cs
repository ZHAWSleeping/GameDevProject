using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.Persistent;
using Gamedev.Main.UI.Menu;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class FileSelect : ScrollableMenu<object, GameState>
{
	[Export]
	private PackedScene SaveFilePanelScene;

	protected override event Action<GameState> HideEvent
	{
		add
		{
			PersistentEvents.SaveSelected += value;
			PersistentEvents.LevelSelected += value;
		}
		remove
		{
			PersistentEvents.SaveSelected -= value;
			PersistentEvents.LevelSelected -= value;
		}
	}

	private Dictionary<Action<object>, Action> ShowEventDict = new();
	protected override event Action<object> ShowEvent
	{
		add
		{
			ShowEventDict.Add(value, () => value(null));
			PersistentEvents.FileSelectOpened += ShowEventDict[value];
		}
		remove
		{
			PersistentEvents.FileSelectOpened -= ShowEventDict[value];
			ShowEventDict.Remove(value);
		}
	}

	protected override void GenerateChildren(object _)
	{
		Scrollable.Instance.GetChildren().ToList().ForEach(c => c.QueueFree());
		for (int i = 0; i < SaveManager.Slots; i++)
		{
			SaveFilePanel panel = SaveFilePanelScene.Instantiate<SaveFilePanel>();
			SaveFile save = SaveManager.Load(i);
			panel.State = new GameState
			{
				File = save,
				CurrentWorld = save.World,
				CurrentLevel = save.Level,
				CurrentRoom = save.Room,
			};
			Scrollable.Instance.AddChild(panel);
		}
	}
}
