using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.Peristent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;
using System.Linq;

public partial class FileSelect : HBoxContainer
{
	private const float Duration = 0.1f;

	[Export]
	private PackedScene SaveFilePanelScene;

	[Export]
	private Control Container;
	private IScrollable Scrollable;

	private Tween Tween;
	private Action<SaveFile> DelegateHide;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		PersistentEvents.FileSelectOpened += UpdateChildren;
		DelegateHide = (_) => AnimatedHide();
		Scrollable = (IScrollable)Container;
	}

	private void UpdateChildren()
	{
		Scrollable.Instance.GetChildren().ToList().ForEach(c => c.QueueFree());
		foreach (var save in SaveManager.SaveFiles.Values)
		{
			SaveFilePanel panel = SaveFilePanelScene.Instantiate<SaveFilePanel>();
			panel.File = save;
			Scrollable.Instance.AddChild(panel);
		}
		Scrollable.RefreshChildren();
		AnimatedShow();
	}


	private void AnimatedShow()
	{
		PersistentEvents.SaveSelected += DelegateHide;
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
		PersistentEvents.SaveSelected -= DelegateHide;
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
