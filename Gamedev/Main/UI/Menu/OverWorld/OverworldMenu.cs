using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.Peristent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;
using System.Linq;

public partial class OverworldMenu : Control
{
	private const float Duration = 0.1f;
	[Export]
	private PackedScene WorldScene;
	[Export]
	private Control Container;
	private IScrollable Scrollable;
	private Tween Tween;
	private Action<SaveFile, int> DelegateHide;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Scrollable = (IScrollable)Container;
		DelegateHide = (_, _) => AnimatedHide();
		PersistentEvents.SaveSelected += UpdateChildren;
	}

	private void UpdateChildren(SaveFile file)
	{
		Scrollable.Instance.GetChildren().ToList().ForEach(c => c.QueueFree());
		int i = 0;
		foreach (var world in file.CompletedLevels)
		{
			WorldPanel panel = WorldScene.Instantiate<WorldPanel>();
			panel.UpdateChildren(file, i);
			Scrollable.Instance.AddChild(panel);
			i++;
		}
		Scrollable.RefreshChildren();
		AnimatedShow();
	}


	private void AnimatedShow()
	{
		PersistentEvents.WorldSelected += DelegateHide;
		Scrollable.Instance.SetProcessModeDeferred(ProcessModeEnum.Inherit);
		this.SetProcessModeDeferred(ProcessModeEnum.Inherit);
		if (Tween != null)
			Tween.Stop();
		Tween = CreateTween();
		Tween.TweenProperty(
			this,
			PropertyName.Modulate.ToString(),
			Colors.White,
			Duration
		);
	}

	private void AnimatedHide()
	{
		PersistentEvents.WorldSelected -= DelegateHide;
		Scrollable.Instance.SetProcessModeDeferred(ProcessModeEnum.Disabled);
		if (Tween != null)
			Tween.Stop();
		Tween = CreateTween();
		Tween.TweenProperty(
			this,
			PropertyName.Modulate.ToString(),
			Colors.Transparent,
			Duration
		);
		Tween.TweenCallback(Callable.From(() => this.SetProcessModeDeferred(ProcessModeEnum.Disabled)));
	}
}
