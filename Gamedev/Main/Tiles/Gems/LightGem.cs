using Gamedev.Main.Characters.Players;
using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Tiles.Gems
{
	/// <summary>
	/// Crystal that lights up when the palyer touches it and has a battery to power it.
	/// </summary>
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
			BodyEntered += CheckForBattery;
			CollisionEvents.LightActivated += LightUpGem;
		}

		private void LightUpGem()
		{
			GemSprite.Frame = 1;
			GemSprite.Modulate = LitModulate;
			PrimaryLight.Enabled = true;
			SecondaryLight.Enabled = true;
			CollisionEvents.LightActivated -= LightUpGem;
			PingSound.Play();
		}

		private void CheckForBattery(Node2D _)
		{
			CollisionEvents.OnLightTouched();
		}

	}
}
