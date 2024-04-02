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

		private enum AnimationState
		{
			Idle,
			Walk,
			Jump,
			Fall,
			Land,
			Wall,
		}

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
		private WallCheckerNode wallChecker;

		[Export]
		private AnimationTree AnimTree;

		[Export]
		private Sprite2D Sprite;

		private AnimationNodeStateMachinePlayback Animations;
		private bool canWallJump = true;
		private bool isFalling = false;

		public override void _Ready()
		{
			base._Ready();
			CollisionEvents.CollisionDeath += Die;
			GD.Print(WallJumpVelocity);
			Animations = AnimTree.GetStateMachinePlayback();
			//ProcessMode = ProcessModeEnum.Inherit;
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
				if (isFalling && wallChecker.IsOnAnyWall)
				{
					velocity = new Vector2(0, WallSlideSpeed) * (float)delta;
					Animations.Travel(AnimationState.Wall.ToString());
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

			// Just landed
			if (IsOnFloor() && isFalling)
			{
				Animations.Travel(AnimationState.Land.ToString());
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
				if (IsOnFloor())
				{
					velocity.X = direction.X * Speed;
					Animations.Travel(AnimationState.Walk.ToString());
				}
				else
				{
					velocity.X += direction.X * Speed / 12;
				}

				if (MathF.Abs(velocity.X) > Speed)
				{
					velocity.X = Speed * Mathf.Sign(velocity.X);
				}

			}
			else if (IsOnFloor())
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
				Animations.Travel(AnimationState.Idle.ToString());

			}

			// Handle Wall Jump.
			if (Input.IsActionJustPressed("Jump") && wallChecker.IsOnAnyWall && !IsOnFloor())
			{
				float whichSideOnWall = 0.0f;

				if (wallChecker.IsOnlyOnLeftWall)
				{
					whichSideOnWall = WallJumpVelocityX;
				}
				else if (wallChecker.IsOnlyOnRightWall)
				{
					whichSideOnWall = -WallJumpVelocityX;
				}

				velocity = new Vector2(whichSideOnWall, WallJumpVelocity);
				GD.Print(velocity + " and " + WallJumpVelocity);
				//canWallJump = false; timer
			}

			Velocity = velocity;

			if (Velocity.Y > 0)
			{
				isFalling = true;
				if (!wallChecker.IsOnAnyWall)
				{
					Animations.Travel(AnimationState.Fall.ToString());
				}
			}
			else
			{
				isFalling = false;
			}

			if (direction.X > 0)
			{
				Sprite.FlipH = false;
			}
			else if (direction.X < 0)
			{
				Sprite.FlipH = true;
			}


			// Debug stuff
			DebugEvents.OnPlayerSpeed(Velocity);
			DebugEvents.OnPlayerGrounded(IsOnFloor());
			DebugEvents.OnPlayerFalling(isFalling);
			DebugEvents.OnPlayerWall(IsOnWall());
			VectorExtensions.Direction? wallSide = null;
			if (wallChecker.IsOnlyOnLeftWall)
			{
				wallSide = VectorExtensions.Direction.West;
			}
			else if (wallChecker.IsOnlyOnRightWall)
			{
				wallSide = VectorExtensions.Direction.East;
			}
			DebugEvents.OnPlayerWallSide(wallSide);

			MoveAndSlide();
		}
	}
}
