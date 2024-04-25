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

		public enum State
		{
			Grounded,
			Jumping,
			Falling,
			Wall,
			Invalid,
		}

		[Export]
		public PlayerData Data;

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

		private PlayerDataOld OldData;

		private float Width = 0.0f;
		private float Height = 0.0f;

		public override void _Ready()
		{
			base._Ready();
			CollisionEvents.CollisionDeath += Die;
			CollisionEvents.BatteryCollected += BatteryCollected;
			CollisionEvents.LightTouched += CheckForBatteries;
			CollisionEvents.CollectedPowerUp += ActivatePowerUp;

			OldData = new(this);
			OldData.CoyoteTime = CoyoteTimeFrames;
			OldData.JumpTime = JumpFrames;
			OldData.MovementSpeed = Speed;

			RectangleShape2D rectangle = (RectangleShape2D)Shape.Shape;
			Width = rectangle.Size.X;
			Height = rectangle.Size.Y;

			Data.Player = this;
			Data.Sprite = Sprite;
			Data.LeftWallCast = LeftWallCast;
			Data.RightWallCast = RightWallCast;
			Data.Shape = (RectangleShape2D)Shape.Shape;
		}


		public override void _PhysicsProcess(double delta)
		{
			OldData.Velocity = Velocity;
			OldData.InputDirection = InputExtensions.MovementVector();
			OldData.JumpHeld = Inputs.Jump.Pressed();
			OldData.JumpJustPressed = Inputs.Jump.JustPressed();
			OldData.Delta = delta;

			if (LeftWallCast.GetCollisions().HasNodesInGroup("Walls"))
			{
				OldData.WallSide = VectorExtensions.Direction.West;
			}
			else if (RightWallCast.GetCollisions().HasNodesInGroup("Walls"))
			{
				OldData.WallSide = VectorExtensions.Direction.East;
			}
			else
			{
				OldData.WallSide = VectorExtensions.Direction.None;
			}


			switch (OldData.State)
			{
				case State.Grounded:
					HandleGrounded();
					break;
				case State.Falling:
					HandleFalling();
					break;
				case State.Jumping:
					HandleJumping();
					break;
				case State.Wall:
					HandleWall();
					break;
			}

			if (OldData.Velocity.ToQuadrantDirection() == VectorExtensions.Direction.East)
			{
				Sprite.FlipH = false;
			}
			else if (OldData.Velocity.ToQuadrantDirection() == VectorExtensions.Direction.West)
			{
				Sprite.FlipH = true;
			}

			Velocity = OldData.Velocity;
			UpdateDebug();
			MoveAndSlide();
		}

		private void HandleGrounded()
		{
			
		}

		private void HandleJumping()
		{
			if (!OldData.JumpHeld || OldData.JumpTime <= 0)
			{
				OldData.PreviousState = OldData.State;
				OldData.State = State.Falling;
				HandleFalling();
				return;
			}

			if (IsOnWall())
			{
				OldData.PreviousState = OldData.State;
				OldData.State = State.Wall;
				HandleWall();
				return;
			}

			OldData.JumpTime--;

			// Accelerate
			if (OldData.InputDirection == Vector2.Zero)
			{
				OldData.Velocity = new(Mathf.MoveToward(Velocity.X, 0, Speed * AirborneModifier), OldData.Velocity.Y);
			}
			else
			{
				OldData.Velocity = new(OldData.InputDirection.X * Speed, OldData.Velocity.Y);
			}

			switch (OldData.PreviousState)
			{
				case State.Grounded:
					OldData.Velocity = new(OldData.Velocity.X, JumpVelocity);
					Sprite.Travel(AnimationState.Jump);
					break;
				case State.Wall:
					Sprite.Travel(AnimationState.Jump);

					break;
				default:
					OldData.Velocity = new(OldData.Velocity.X, OldData.Velocity.Y - 500 * (float)OldData.Delta);
					OldData.Velocity += GetGravity() * (float)OldData.Delta;
					break;
			}

		}

		private void HandleFalling()
		{
			if (IsOnWall())
			{
				OldData.PreviousState = OldData.State;
				OldData.State = State.Wall;
				HandleWall();
				return;
			}

			if (IsOnFloor())
			{
				OldData.PreviousState = OldData.State;
				OldData.State = State.Grounded;
				HandleGrounded();
				return;
			}

			// Slow down and accelerate
			if (OldData.InputDirection == Vector2.Zero)
			{
				OldData.Velocity = new(Mathf.MoveToward(Velocity.X, 0, Speed * AirborneModifier), OldData.Velocity.Y);
			}
			else
			{
				OldData.Velocity = new(OldData.InputDirection.X * Speed, OldData.Velocity.Y);
			}

			OldData.Velocity += GetGravity() * (float)OldData.Delta;
			Sprite.Travel(AnimationState.Fall);

		}

		private void HandleWall()
		{
			if (OldData.JumpJustPressed)
			{
				OldData.PreviousState = OldData.State;
				OldData.State = State.Jumping;
				HandleJumping();
				return;
			}

			if (OldData.WallSide == VectorExtensions.Direction.None || OldData.InputDirection.ToQuadrantDirection() != OldData.WallSide)
			{
				OldData.PreviousState = OldData.State;
				OldData.State = State.Falling;
				HandleFalling();
				return;
			}


			Position = new(MathF.Round(Position.X), Position.Y);
			ResetTimers();
			OldData.Velocity = GetGravity() * 0.5f * (float)OldData.Delta;
			Sprite.Travel(AnimationState.Wall);

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
			DebugEvents.OnPlayerDataEvent(OldData);
		}
	}
}
