using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Characters.Player
{
	public partial class Player : CharacterBody2D
	{
		[Export]
		private float Speed = 300.0f;
		[Export]
		private float JumpVelocity = -400.0f;
		public override void _Ready()
		{
			base._Ready();
			CollisionEvents.CollisionDeath += Die;
		}


		private void Die(){
			
		}


		public override void _PhysicsProcess(double delta)
		{
			Vector2 velocity = Velocity;

			// Add the gravity.
			if (!IsOnFloor())
			{
				velocity += GetGravity() * (float)delta;
			}

			// Handle Jump.
			if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			{
				velocity.Y = JumpVelocity;
			}

			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}

			Velocity = velocity;
			MoveAndSlide();
		}
	}
}
