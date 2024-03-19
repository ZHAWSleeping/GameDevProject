using Godot;
using System;

public partial class GameOver : TextureRect
{
	[Export]
	private float BlurAmount = 3.0f;
	[Export]
	private float BlurDuration = 1.0f;

	private static StringName BlurAmountName = "lod";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Tween tween = CreateTween();
		tween.SetTrans(Tween.TransitionType.Linear);
		tween.TweenMethod
		(
			Callable.From((float blur) => (Material as ShaderMaterial).SetShaderParameter(BlurAmountName, blur)),
			0.0f,
			BlurAmount,
			BlurDuration
		).SetTrans(Tween.TransitionType.Linear);
	}
}
