using Gamedev.Events;
using Gamedev.Main.Constants;
using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Godot;
using System;
using static Gamedev.Main.Characters.Player.PlayerSprite;

namespace Gamedev.Main.Characters.Player
{
	public partial class PlayerOld : CharacterBody2D
	{


		public enum SpecialAction
		{
			None,
			WallJump,
			Dash,
		}

		public enum PlayerState
		{
			Grounded,
			Jumping,
			Falling,
			Wall,
		}

		[Export]
		private float Speed = 300.0f;

		[Export]
		private float JumpVelocity = -400.0f;
		[Export]
		private float JumpMidAirIncrease = -100.0f;

		[Export]
		private float WallJumpVelocity = -200.0f;

		[Export]
		private float WallJumpVelocityX = 250.0f;

		[Export]
		private float WallSlideSpeed = 700.0f;

		[Export]
		private WallCheckerNode wallChecker;

		[Export]
		private PlayerSprite Sprite;

		[Export]
		public int BatteryCount = 0;
		[Export]
		public SpecialAction specialAction = SpecialAction.None;


		private bool canWallJump = true;
		private bool isFalling = false;
		private int jumpTimer = 15;

		public override void _Ready()
		{
			base._Ready();
			CollisionEvents.CollisionDeath += Die;
			CollisionEvents.BatteryCollected += BatteryCollected;
			CollisionEvents.LightTouched += CheckForBatteries;
			CollisionEvents.CollectedPowerUp += ActivatePowerUp;
		}

		private void Die()
		{
			StateEvents.OnRestartRequested();
			//Deathsound
			Modulate = new Color(4, 1, 1, 1);
			this.SetProcessModeDeferred(ProcessModeEnum.Disabled);
		}

		private void BatteryCollected()
		{
			BatteryCount++;
		}

		private void CheckForBatteries()
		{
			if (BatteryCount > 0)
			{
				CollisionEvents.OnActivateLight();
				BatteryCount--;
			}
		}

		private void ActivatePowerUp()
		{
			specialAction = SpecialAction.WallJump;
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
					Sprite.Travel(AnimationState.Wall);
				}
				else
				{
					if (Input.IsActionPressed("Jump") && (jumpTimer > 0))
					{
						jumpTimer--;
						velocity.Y -= 500 * (float)delta;
					}
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
				Sprite.Travel(AnimationState.Land);
			}

			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			Vector2 direction = Input.GetVector(
				Inputs.West.ToString(),
				Inputs.East.ToString(),
				Inputs.North.ToString(),
				Inputs.South.ToString()
			);

			if (direction != Vector2.Zero)
			{
				if (IsOnFloor())
				{
					velocity.X = direction.X * Speed;
					Sprite.Travel(AnimationState.Walk);
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
				Sprite.Travel(AnimationState.Idle);

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
				//canWallJump = false; timer
			}

			Velocity = velocity;

			if (Velocity.Y > 0)
			{
				isFalling = true;
				if (!wallChecker.IsOnAnyWall)
				{
					Sprite.Travel(AnimationState.Fall);
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

			if (IsOnFloor() || IsOnWall())
			{
				jumpTimer = 15;
			}

			MoveAndSlide();
		}
	}
}
