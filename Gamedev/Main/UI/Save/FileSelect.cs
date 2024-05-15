using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.Peristent;
using Gamedev.Main.UI.Menu;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public record class LevelData
{
	public int World;
	public int Level;
}

public partial class FileSelect : ScrollableMenu<object, SaveFile>
{
	[Export]
	private PackedScene SaveFilePanelScene;

	protected override IScrollable Scrollable { get; set; }
	protected override Tween Tween { get; set; }

	protected override event Action<SaveFile> HideEvent
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
		foreach (var save in SaveManager.SaveFiles.Values)
		{
			SaveFilePanel panel = SaveFilePanelScene.Instantiate<SaveFilePanel>();
			panel.File = save;
			Scrollable.Instance.AddChild(panel);
		}
	}
}
