using System;
using System.Linq;
using Gamedev.Main.Audio;
using Gamedev.Main.Constants;
using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.UI.Scrollable;
using Godot;

namespace Gamedev.Main.UI.Menu
{
	/// <summary>
	/// Can generate children and has different states for show, hide.
	/// Params are used for events.
	/// </summary>
	/// <typeparam name="TShow"></typeparam> state when shown
	/// <typeparam name="THide"></typeparam> state when hidden
	public abstract partial class ScrollableMenu<TShow, THide> : Control
	{
		[Export]
		protected Control Container;

		protected virtual float Duration { get; } = 0.1f;
		protected abstract event Action<THide> HideEvent;
		protected abstract event Action<TShow> ShowEvent;
		protected IScrollable Scrollable { get; set; }
		protected Tween Tween { get; set; }



		public override void _Ready()
		{
			base._Ready();
			ShowEvent += UpdateChildren;
			Scrollable = (IScrollable)Container;
		}

		public override void _Process(double delta)
		{
			base._Process(delta);
			if (Input.IsActionJustPressed(Inputs.Cancel.ToString()) && MenuStack.History.TryPop(out Action previous))
			{
				HideEvent -= HideCallback;
				AnimatedHide();
				previous();
				PersistentAudioEvents.OnAudioRequested(GlobalAudioManager.Sound.MenuBack);
			}
		}

		protected abstract void GenerateChildren(TShow showObject);

		protected void UpdateChildren(TShow showObject)
		{
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
			//Scrollable.Instance.SetProcessModeDeferred(ProcessModeEnum.Inherit);
			//this.SetProcessModeDeferred(ProcessModeEnum.Inherit);
			Scrollable.Instance.ProcessMode = ProcessModeEnum.Inherit;
			ProcessMode = ProcessModeEnum.Inherit;
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
			//Scrollable.Instance.SetProcessModeDeferred(ProcessModeEnum.Disabled);
			Scrollable.Instance.ProcessMode = ProcessModeEnum.Disabled;
			if (Tween != null)
				Tween.Stop();
			Tween = CreateTween();
			Tween.TweenProperty(
				this,
				PropertyName.Modulate.ToString(),
				Colors.Transparent,
				Duration
			);
			//Tween.TweenCallback(Callable.From(() => this.SetProcessModeDeferred(ProcessModeEnum.Disabled)));
			Tween.TweenCallback(Callable.From(() => ProcessMode = ProcessModeEnum.Disabled));
		}
	}
}