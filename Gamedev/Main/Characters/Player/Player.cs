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
		private int CoyoteTimeFrames = 15;

		[Export]
		private int JumpFrames = 20;

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
			MoveAndSlide();
		}

		private void HandleGrounded()
		{
			GD.Print(Data.State.ToString());
			// Transition to Jumping state if the jump button is held
			if (Data.JumpHeld)
			{
				Data.State = PlayerState.Jumping;
				Data.PreviousState = Data.State;
				HandleJumping();
				return;
			}

			// Transition to the Falling state if we don't have a floor anymore and the coyote timer has expired
			if (!IsOnFloor() && Data.CoyoteTime <= 0)
			{
				Data.State = PlayerState.Falling;
				Data.PreviousState = Data.State;
				HandleFalling();
				return;
			}

			// Reset coyote timer and jump timer or count down coyote
			if (IsOnFloor())
			{
				Data.JumpTime = JumpFrames;
				Data.CoyoteTime = CoyoteTimeFrames;
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
			GD.Print(Data.State.ToString());
			if (!Data.JumpHeld || Data.JumpTime <= 0)
			{
				Data.State = PlayerState.Falling;
				Data.PreviousState = Data.State;
				HandleFalling();
				return;
			}

			if (IsOnWall())
			{
				Data.State = PlayerState.Wall;
				Data.PreviousState = Data.State;
				HandleWall();
				return;
			}
		}

		private void HandleFalling()
		{
			GD.Print(Data.State.ToString());
			if (IsOnWall())
			{
				Data.State = PlayerState.Wall;
				Data.PreviousState = Data.State;
				HandleWall();
				return;
			}

		}

		private void HandleWall()
		{
			GD.Print(Data.State.ToString());
			if (Data.JumpHeld)
			{
				Data.State = PlayerState.Jumping;
				Data.PreviousState = Data.State;
				HandleJumping();
				return;
			}

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


	}
}
