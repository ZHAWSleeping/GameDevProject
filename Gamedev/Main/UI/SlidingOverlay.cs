using Gamedev.Main.Events;
using Godot;
using System;

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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SlideIn(Tween tween)
	{
		tween.TweenProperty(this, PropertyName.Position.ToString(), Vector2.Zero, SlideDuration).SetDelay(0.2f);
	}

	public void SlideOut(Tween tween)
	{
		tween.TweenProperty(this, PropertyName.Position.ToString(), new Vector2(0, (int)Size.Y), SlideDuration).SetDelay(0.2f);
	}


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

	public void Transition(Callable callback)
	{
		Tween tween = CreateTween();
		SlideIn(tween);
		tween.TweenCallback(callback).SetDelay(1.0f);
	}
}
