using System;
using Gamedev.Events;
using Gamedev.Main.Constants;
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

		[Export]
		private CollisionShape2D Shape;

		[Export]
		private ShapeCast2D LeftWallCast;

		[Export]
		private ShapeCast2D RightWallCast;

		private PlayerData Data;

		private float Width = 0.0f;
		private float Height = 0.0f;

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

			RectangleShape2D rectangle = (RectangleShape2D)Shape.Shape;
			Width = rectangle.Size.X;
			Height = rectangle.Size.Y;
		}


		public override void _PhysicsProcess(double delta)
		{
			Data.Velocity = Velocity;
			Data.InputDirection = InputExtensions.MovementVector();
			Data.JumpHeld = Inputs.Jump.Pressed();
			Data.JumpJustPressed = Inputs.Jump.JustPressed();
			Data.Delta = delta;

			if (LeftWallCast.GetCollisions().HasNodesInGroup("Walls"))
			{
				Data.WallSide = VectorExtensions.Direction.West;
			}
			else if (RightWallCast.GetCollisions().HasNodesInGroup("Walls"))
			{
				Data.WallSide = VectorExtensions.Direction.East;
			}
			else
			{
				Data.WallSide = VectorExtensions.Direction.None;
			}


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

			if (Data.Velocity.ToQuadrantDirection() == VectorExtensions.Direction.East)
			{
				Sprite.FlipH = false;
			}
			else if (Data.Velocity.ToQuadrantDirection() == VectorExtensions.Direction.West)
			{
				Sprite.FlipH = true;
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
					Sprite.Travel(AnimationState.Jump);
					break;
				case PlayerState.Wall:
					Sprite.Travel(AnimationState.Jump);

					break;
				default:
					Data.Velocity = new(Data.Velocity.X, Data.Velocity.Y - 500 * (float)Data.Delta);
					Data.Velocity += GetGravity() * (float)Data.Delta;
					break;
			}

		}

		private void HandleFalling()
		{
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
			Sprite.Travel(AnimationState.Fall);

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

			if (Data.WallSide == VectorExtensions.Direction.None || Data.InputDirection.ToQuadrantDirection() != Data.WallSide)
			{
				Data.PreviousState = Data.State;
				Data.State = PlayerState.Falling;
				HandleFalling();
				return;
			}


			Position = new(MathF.Round(Position.X), Position.Y);
			ResetTimers();
			Data.Velocity = GetGravity() * 0.5f * (float)Data.Delta;
			Sprite.Travel(AnimationState.Wall);

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
