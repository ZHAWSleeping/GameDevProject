using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.UI
{
	public partial class SlidingOverlay : TextureRect
	{
		[Export]
		private float SlideDuration = 0.25f;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Visible = true;
			Tween tween = CreateTween();
			SlideOut(tween);

		}

		/// <summary>
		/// Tween to slide in an overlay
		/// </summary>
		/// <param name="tween"></param> tween for the overlay
		public void SlideIn(Tween tween)
		{
			tween.TweenProperty(this, PropertyName.Position.ToString(), Vector2.Zero, SlideDuration).SetDelay(0.2f);
		}

		/// <summary>
		/// Tween to remove a overlay
		/// </summary>
		/// <param name="tween"></param> tween for the overlay
		public void SlideOut(Tween tween)
		{
			tween.TweenProperty(this, PropertyName.Position.ToString(), new Vector2(0, (int)Size.Y), SlideDuration).SetDelay(0.2f);
		}


		/// <summary>
		/// Reloads the current scene
		/// </summary>
		/// <param name="_"></param>
		public void RestartGame(Vector2 _)
		{
			Tween tween = CreateTween();
			SlideIn(tween);
			tween
			.TweenCallback(Callable.From(() =>
			{
				GetTree().ReloadCurrentScene();
				CollisionEvents.Clear();
				//StateEvents.Clear();
				DebugEvents.Clear();
			}))
			.SetDelay(1.0f);
		}

		/// <summary>
		/// Tweene transition between scenes
		/// </summary>
		/// <param name="callback"></param>
		public void Transition(Callable callback)
		{
			Tween tween = CreateTween();
			SlideIn(tween);
			tween.TweenCallback(callback).SetDelay(1.0f);
		}
	}
}