using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.Peristent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;
using System.Linq;

public partial class MainMenu : Control
{

	private const float Duration = 0.1f;

	[Export]
	private Control Container;

	private IScrollable Scrollable;
	private Tween Tween;

	public override void _Ready()
	{
		Scrollable = (IScrollable)Container;
		AnimatedShow();
	}

	private void AnimatedShow()
	{
		PersistentEvents.FileSelectOpened += AnimatedHide;
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
		PersistentEvents.FileSelectOpened -= AnimatedHide;
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
	}}
