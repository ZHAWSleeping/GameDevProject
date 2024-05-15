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

public partial class FileSelect : ScrollableMenu<object, LevelData>
{
	protected override IScrollable Scrollable { get; set; }
	protected override Tween Tween { get; set; }

	protected override event Action<LevelData> HideEvent
	{
		add
		{
			PersistentEvents.LevelSelected2 += value;
		}
		remove
		{
			PersistentEvents.LevelSelected2 -= value;
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

	protected override void HideCallback(LevelData _)
	{
		AnimatedHide();
	}
}
