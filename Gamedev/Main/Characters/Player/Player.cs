using System;
using Gamedev.Events;
using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Godot;
using static Gamedev.Main.Characters.Player.PlayerSprite;

namespace Gamedev.Main.Characters.Player
{
	public partial class Player : CharacterBody2D
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
		private int CoyoteTimeFrames = 5;

		[Export]
		private int JumpFrames = 20;

		[Export]
		private float AirborneModifier;

		[Export]
		private WallCheckerNode wallChecker;

		[Export]
		private PlayerSprite Sprite;

		[Export]
		public int BatteryCount = 0;
		[Export]
		public SpecialAction specialAction = SpecialAction.None;

		private PlayerData Data;

		private bool canWallJump = true;

		public override void _Ready()
		{
			base._Ready();
			CollisionEvents.CollisionDeath += Die;
			CollisionEvents.BatteryCollected += BatteryCollected;
			CollisionEvents.LightTouched += CheckForBatteries;
			CollisionEvents.CollectedPowerUp += ActivatePowerUp;
			Data = new();
			Data.CoyoteTime = CoyoteTimeFrames;
			Data.JumpTime = JumpFrames;
		}


		public override void _PhysicsProcess(double delta)
		{
			Data.Velocity = Velocity;
			Data.InputDirection = InputExtensions.MovementVector();
			Data.JumpHeld = Inputs.Jump.Pressed();
			Data.JumpJustPressed = Inputs.Jump.JustPressed();
			Data.Delta = delta;

			switch (Data.State)
			{
				case PlayerState.Grounded:
					HandleGrounded();
					break;
				case PlayerState.Falling:
					HandleFalling();
					break;
				case PlayerState.Jumping:
					HandleJumping();
					break;
				case PlayerState.Wall:
					HandleWall();
					break;
			}

			Velocity = Data.Velocity;
			UpdateDebug();
			MoveAndSlide();
		}

		private void HandleGrounded()
		{
			// Transition to Jumping state if the jump button is held
			if (Data.JumpJustPressed)
			{
				Data.PreviousState = Data.State;
				Data.State = PlayerState.Jumping;
				HandleJumping();
				return;
			}

			// Transition to the Falling state if we don't have a floor anymore and the coyote timer has expired
			if (!IsOnFloor() && Data.CoyoteTime <= 0)
			{
				Data.PreviousState = Data.State;
				Data.State = PlayerState.Falling;
				HandleFalling();
				return;
			}

			// Reset coyote timer and jump timer or count down coyote
			if (IsOnFloor())
			{
				ResetTimers();
			}
			else
			{
				Data.CoyoteTime--;
			}

			// Slow down and accelerate
			if (Data.InputDirection == Vector2.Zero)
			{
				Data.Velocity = new(Mathf.MoveToward(Velocity.X, 0, Speed), 0.0f);
				Sprite.Travel(AnimationState.Idle);
			}
			else
			{
				Data.Velocity = new(Data.InputDirection.X * Speed, 0.0f);
				Sprite.Travel(AnimationState.Walk);
			}

			// Clamp speed
			if (MathF.Abs(Data.Velocity.X) > Speed)
			{
				Data.Velocity = new(Speed * Mathf.Sign(Data.Velocity.X), 0.0f);
			}
		}

		private void HandleJumping()
		{
			GD.Print("jumpojg");
			if (!Data.JumpHeld || Data.JumpTime <= 0)
			{
				Data.PreviousState = Data.State;
				Data.State = PlayerState.Falling;
				HandleFalling();
				return;
			}

			if (IsOnWall())
			{
				Data.PreviousState = Data.State;
				Data.State = PlayerState.Wall;
				HandleWall();
				return;
			}

			Data.JumpTime--;

			// Accelerate
			if (Data.InputDirection == Vector2.Zero)
			{
				Data.Velocity = new(Mathf.MoveToward(Velocity.X, 0, Speed * AirborneModifier), Data.Velocity.Y);
			}
			else
			{
				Data.Velocity = new(Data.InputDirection.X * Speed, Data.Velocity.Y);
			}

			switch (Data.PreviousState)
			{
				case PlayerState.Grounded:
					Data.Velocity = new(Data.Velocity.X, JumpVelocity);
					break;
				case PlayerState.Wall:
					break;
				default:
					Data.Velocity = new(Data.Velocity.X, Data.Velocity.Y - 500 * (float)Data.Delta);
					Data.Velocity += GetGravity() * (float)Data.Delta;
					break;
			}

		}

		private void HandleFalling()
		{
			GD.Print("Falling");
			if (IsOnWall())
			{
				Data.PreviousState = Data.State;
				Data.State = PlayerState.Wall;
				HandleWall();
				return;
			}

			if (IsOnFloor())
			{
				Data.PreviousState = Data.State;
				Data.State = PlayerState.Grounded;
				HandleGrounded();
				return;
			}

			// Slow down and accelerate
			if (Data.InputDirection == Vector2.Zero)
			{
				Data.Velocity = new(Mathf.MoveToward(Velocity.X, 0, Speed * AirborneModifier), Data.Velocity.Y);
			}
			else
			{
				Data.Velocity = new(Data.InputDirection.X * Speed, Data.Velocity.Y);
			}

			Data.Velocity += GetGravity() * (float)Data.Delta;

		}

		private void HandleWall()
		{
			if (Data.JumpJustPressed)
			{
				Data.PreviousState = Data.State;
				Data.State = PlayerState.Jumping;
				HandleJumping();
				return;
			}

		}


		private void ResetTimers()
		{
			Data.JumpTime = JumpFrames;
			Data.CoyoteTime = CoyoteTimeFrames;
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

		private void UpdateDebug()
		{
			DebugEvents.OnPlayerDataEvent(Data);
		}
	}
}
