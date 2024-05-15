using System;
using System.Linq;
using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.UI.Scrollable;
using Godot;

namespace Gamedev.Main.UI.Menu
{
	public abstract partial class ScrollableMenu<TShow, THide> : Control
	{
		[Export]
		protected Control Container;

		protected virtual float Duration { get; } = 0.1f;
		protected abstract event Action<THide> HideEvent;
		protected abstract event Action<TShow> ShowEvent;
		protected abstract IScrollable Scrollable { get; set; }
		protected abstract Tween Tween { get; set; }

		protected abstract void HideCallback(THide _);

		public override void _Ready()
		{
			base._Ready();
			ShowEvent += UpdateChildren;
			Scrollable = (IScrollable)Container;
		}

		protected void AnimatedShow()
		{
			HideEvent += HideCallback;
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

		protected abstract void GenerateChildren();

		protected void UpdateChildren()
		{
			Scrollable.Instance.GetChildren().ToList().ForEach(c => c.QueueFree());
			GenerateChildren();
			Scrollable.RefreshChildren();
			AnimatedShow();
		}

		protected void AnimatedHide()
		{
			HideEvent -= HideCallback;
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