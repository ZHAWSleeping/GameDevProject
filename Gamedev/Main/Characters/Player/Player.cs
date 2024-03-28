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
		private float WallJumpVelocity = -200.0f;
		[Export]
		private float WallJumpVelocityX = 250.0f;
		[Export]
		private float WallSlideSpeed = 700.0f;
		[Export]
		public WallCheckerNode wallChecker;
		private bool canWallJump = true;
		private bool isFalling = false;

		public override void _Ready()
		{
			base._Ready();
			CollisionEvents.CollisionDeath += Die;
			GD.Print(WallJumpVelocity);
		}

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
			//if (IsOnWallOnly() && !isFalling && wallChecker.isOnWall) { 			}


			// Add the gravity.
			if (!IsOnFloor())
			{
				if (!isFalling && wallChecker.isOnAnyWall())
				{
					velocity = new Vector2(0, WallSlideSpeed) * (float)delta;
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

			}
			else if (IsOnFloor())
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}

			// Handle Wall Jump.
			if (Input.IsActionJustPressed("Jump") && wallChecker.isOnAnyWall() && !IsOnFloor())
			{
				float whichSideOnWall;
				GD.Print(wallChecker.leftOrRight());

				if (wallChecker.leftOrRight() == 0)
				{
					whichSideOnWall = WallJumpVelocityX;
				}
				else
				{
					whichSideOnWall = -WallJumpVelocityX;
				}

				velocity = new Vector2(whichSideOnWall, WallJumpVelocity);
				GD.Print(velocity + " and " + WallJumpVelocity);
				//canWallJump = false; timer
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
