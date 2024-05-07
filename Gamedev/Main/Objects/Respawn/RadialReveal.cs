using Gamedev.Constants;
using Godot;
using System;

public partial class RadialReveal : Sprite2D
{
	[Export]
	private float Duration = 5.0f;
	private static string FillFromPath = $"{Sprite2D.PropertyName.Texture}/{GradientTexture2D.PropertyName.FillFrom}";
	private Tween Animation;
	public override void _Ready()
	{
		Hide(GlobalPosition + new Vector2(320/2, 320/2));
	}

	private Vector2 MapToTextureSpace(Vector2 position)
	{
		return (position - GlobalPosition) / Texture.GetSize();
	}

	public void Hide(Vector2 position)
	{
		Vector2 target = MapToTextureSpace(position);
		((GradientTexture2D)Texture).FillFrom = target;
		((GradientTexture2D)Texture).FillTo = new Vector2(2,2);
		if (Animation != null)
		{
			Animation.Stop();
		}
		Animation = CreateTween();
		Animation.TweenProperty(
			Texture,
			GradientTexture2D.PropertyName.FillTo.ToString(),
			target - new Vector2(0.001f, 0.001f),
			Duration
		)
		.SetEase(Tween.EaseType.InOut)
		.SetTrans(Tween.TransitionType.Cubic);
	}

	public void Show(Vector2 position)
	{
		Vector2 target = MapToTextureSpace(position);
		((GradientTexture2D)Texture).FillFrom = target;
		((GradientTexture2D)Texture).FillTo = target - new Vector2(0.001f, 0.001f);
		if (Animation != null)
		{
			Animation.Stop();
		}
		Animation = CreateTween();
		Animation.TweenProperty(
			Texture,
			GradientTexture2D.PropertyName.FillTo.ToString(),
			new Vector2(2,2),
			Duration
		)
		.SetEase(Tween.EaseType.InOut)
		.SetTrans(Tween.TransitionType.Cubic);

	}
}
