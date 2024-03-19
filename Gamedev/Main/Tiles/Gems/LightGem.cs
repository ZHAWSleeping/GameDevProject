using Godot;
using System;

namespace Gamedev.Main.Tiles.Gems
{
	public partial class LightGem : Area2D
	{
		[Export]
		AnimatedSprite2D GemSprite;

		[Export]
		PointLight2D PrimaryLight;

		[Export]
		PointLight2D SecondaryLight;

		[Export]
		Color LitModulate;

		[Export]
		private AudioStreamPlayer2D PingSound;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			BodyEntered += LightUpGem;
		}

		private void LightUpGem(Node2D _) {
			GemSprite.Frame = 1;
			GemSprite.Modulate = LitModulate;
			PrimaryLight.Enabled = true;
			SecondaryLight.Enabled = true;
			BodyEntered -= LightUpGem;
			PingSound.Play();
		}

	}
}
