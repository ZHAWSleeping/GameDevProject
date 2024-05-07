using Gamedev.Events;
using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Godot;
using System.Linq;

namespace Gamedev.Main.Characters
{
	public partial class CavePlayer : CharacterBody2D
	{
		[Export]
		private float Speed = 300.0f;
		//[Export]
		//private DirectionalSprite Sprite;
		[Export]
		private Node2D Dot;
		[Export]
		private float SlowSpeed = 0.5f;
		[Export]
		private AudioStreamPlayer2D Footsteps;
		[Export]
		private AudioStreamPlayer2D DeathSound;

		public override void _Ready()
		{
			base._Ready();
			CollisionEvents.CollisionWall += Die;
		}

		public override void _PhysicsProcess(double delta)
		{
			float speedModifier = 1.0f;
			Vector2 velocity = Velocity;

			if (Input.IsActionPressed("Precise"))
			{
				speedModifier = SlowSpeed;
			}

			Vector2 direction = Input.GetVector(
				VectorExtensions.Direction.West.ToString(),
				VectorExtensions.Direction.East.ToString(),
				VectorExtensions.Direction.North.ToString(),
				VectorExtensions.Direction.South.ToString()
			);

			if (direction != Vector2.Zero)
			{
				velocity = direction * Speed * speedModifier;
				Dot.GlobalPosition = GlobalPosition + direction * 10;
				if (!Footsteps.Playing)
				{
					Footsteps.Play();
				}
			}
			else
			{
				velocity = velocity.MoveToward(Vector2.Zero, Speed);
				Dot.GlobalPosition = GlobalPosition;
				Footsteps.Stop();
			}

			if (direction.Length() > 0.0)
			{
				//Sprite.AlignSprite(velocity);
			}

			Velocity = velocity;
			MoveAndSlide();
			KinematicCollision2D[] collisions = this.GetSlideCollisions();
			if (collisions.HasNodesInGroup("Walls"))
			{
				//StateEvents.OnRestartRequested();
				Die();
			}
		}

		private void Die()
		{
			//Sprite.Modulate = new Color(4, 1, 1, 1);
			DeathSound.Play();
			ProcessMode = ProcessModeEnum.Disabled;
		}
	}
}
