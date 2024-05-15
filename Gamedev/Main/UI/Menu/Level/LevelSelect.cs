using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.Peristent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;
using System.Linq;


namespace Gamedev.Main.UI.Menu.Level
{
	public partial class LevelSelect : HBoxContainer
	{
		[Export]
		private PackedScene PanelScene;
		[Export]
		private Label Title;
		[Export]
		private Control Container;
		private IScrollable Scrollable;
		private int World;
		private bool[] Levels;
		private const float Duration = 0.1f;

		private Tween Tween;


		private Action<int, int> DelegateHide;

		public override void _Ready()
		{
			Scrollable = (IScrollable)Container;
			DelegateHide = (_, _) => AnimatedHide();
			PersistentEvents.WorldSelected += UpdateChildren;
		}

		private void UpdateChildren(SaveFile file, int world)
		{
			World = world;
			Levels = file.CompletedLevels[world];
			Scrollable.Instance.GetChildren().ToList().ForEach(c => c.QueueFree());
			int i = 0;
			foreach (var completed in Levels)
			{
				LevelSelectPanel scene = PanelScene.Instantiate<LevelSelectPanel>();
				scene.Level = i;
				i++;
				scene.World = world;
				scene.Completed = completed;
				Scrollable.Instance.AddChild(scene);
			}
			Title.Text = $"World {world}";
			Scrollable.RefreshChildren();
			AnimatedShow();
		}

		private void AnimatedShow()
		{
			PersistentEvents.LevelSelected += DelegateHide;
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
			PersistentEvents.LevelSelected -= DelegateHide;
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
}
