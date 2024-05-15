using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.Persistent;
using Gamedev.Main.UI.Menu;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TitleScreen : ScrollableMenu<object, object>
{
	private Dictionary<Action<object>, Action> HideEventDict = new();
	protected override event Action<object> HideEvent
	{
		add
		{
			HideEventDict.Add(value, () => value(null));
			PersistentEvents.FileSelectOpened += HideEventDict[value];
		}
		remove
		{
			PersistentEvents.FileSelectOpened -= HideEventDict[value];
			HideEventDict.Remove(value);
		}
	}

	private Dictionary<Action<object>, Action> ShowEventDict = new();
	protected override event Action<object> ShowEvent
	{
		add
		{
			ShowEventDict.Add(value, () => value(null));
			PersistentEvents.TitleScreenRequested += ShowEventDict[value];
		}
		remove
		{
			PersistentEvents.TitleScreenRequested -= ShowEventDict[value];
			ShowEventDict.Remove(value);
		}
	}
	public override void _Ready()
	{
		base._Ready();
		PersistentEvents.OnTitleScreenRequested();
	}

	protected override void GenerateChildren(object showObject) { }
}
