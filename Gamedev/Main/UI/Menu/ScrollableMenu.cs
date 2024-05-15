using System;
using System.Linq;
using Gamedev.Main.Constants;
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


		public override void _Ready()
		{
			base._Ready();
			ShowEvent += UpdateChildren;
			Scrollable = (IScrollable)Container;
		}

		public override void _Input(InputEvent @event)
		{
			base._Input(@event);
			if (@event.IsAction(Inputs.Cancel.ToString()) && MenuStack.History.TryPop(out Action previous))
			{
				HideEvent -= HideCallback;
				AnimatedHide();
				previous();
			}
		}

		protected abstract void GenerateChildren(TShow showObject);

		protected void UpdateChildren(TShow showObject)
		{
			Scrollable.Instance.GetChildren().ToList().ForEach(c => c.QueueFree());
			GenerateChildren(showObject);
			Scrollable.RefreshChildren();
			AnimatedShow();
		}

		protected void HideCallback(THide _)
		{
			HideEvent -= HideCallback;
			MenuStack.History.Push(AnimatedShow);
			AnimatedHide();
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

		protected void AnimatedHide()
		{
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