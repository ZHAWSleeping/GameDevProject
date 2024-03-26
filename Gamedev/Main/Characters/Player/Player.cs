using Gamedev.Events;
using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Godot;
using System;
using System.Diagnostics;

namespace Gamedev.Main.Characters.Player
{
	public partial class Player : CharacterBody2D
	{
		[Export]
		private float Speed = 300.0f;
		[Export]
		private float JumpVelocity = -400.0f;
		[Export]
		private float WallJumpVelocity = -150.0f;
		[Export]
		private float WallJumpVelocityX = 300.0f;
		public override void _Ready()
		{
			base._Ready();
			CollisionEvents.CollisionDeath += Die;
		}

		private bool canWallJump = true;
		private bool isFalling = false;


		private void Die()
		{
			StateEvents.OnRestartRequested();
			//Deathsound
			Modulate = new Color(4, 1, 1, 1);
			this.SetProcessModeDeferred(ProcessModeEnum.Disabled);
		}


		public override void _PhysicsProcess(double delta)
		{
			Vector2 velocity = Velocity;

			// Add the gravity.
			if (!IsOnFloor())
			{
				if (IsOnWallOnly() && !isFalling)
				{
					velocity += new Vector2(0, 600) * (float)delta;
				}
				else
				{
					velocity += GetGravity() * (float)delta;
				}

			}

			//needs to change wall or be away from the wall for a sec

			// Handle Jump.
			if (Input.IsActionJustPressed("Jump") && IsOnFloor())
			{
				velocity.Y = JumpVelocity;
			}



			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			Vector2 direction = Input.GetVector(
				VectorExtensions.Direction.West.ToString(),
				VectorExtensions.Direction.East.ToString(),
				VectorExtensions.Direction.North.ToString(),
				VectorExtensions.Direction.South.ToString()
			);
			if (direction != Vector2.Zero)
			{
				if (!IsOnFloor())
				{
					GD.Print(direction.X);
					velocity.X += direction.X * Speed / 12;
				}
				else
				{
					velocity.X = direction.X * Speed;
				}

				if (MathF.Abs(velocity.X) > Speed)
				{
					velocity.X = Speed * Mathf.Sign(velocity.X);
				}


				//if (MathF.Abs(velocity.X) <= (MathF.Abs(direction.X) * Speed))

			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}


			// Handle Wall Jump.
			if (Input.IsActionJustPressed("Jump") && IsOnWallOnly() && canWallJump)
			{
				float whichSideOnWall;
				Godot.KinematicCollision2D[] dir = this.GetSlideCollisions();

				if (Position.X - dir[0].GetPosition().X > 0)
				{
					whichSideOnWall = WallJumpVelocityX;
				}
				else
				{
					whichSideOnWall = -WallJumpVelocityX;
				}

				velocity += new Vector2(whichSideOnWall, WallJumpVelocity);
				//canWallJump = false;

			}

			Velocity = velocity;

			if (Velocity.Y >= 0)
			{
				isFalling = false;
			}
			else { isFalling = true; }


			MoveAndSlide();
		}
	}
}
