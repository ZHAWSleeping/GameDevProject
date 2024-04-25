using System;
using Gamedev.Events;
using Gamedev.Main.Constants;
using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Godot;

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


		[Export]
		public PlayerData Data;

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

		private PlayerFSM StateMachine;

		public override void _Ready()
		{
			base._Ready();
			CollisionEvents.CollisionDeath += Die;
			CollisionEvents.BatteryCollected += BatteryCollected;
			CollisionEvents.LightTouched += CheckForBatteries;
			CollisionEvents.CollectedPowerUp += ActivatePowerUp;

			Data.Player = this;
			Data.Sprite = Sprite;
			Data.LeftWallCast = LeftWallCast;
			Data.RightWallCast = RightWallCast;
			Data.Shape = (RectangleShape2D)Shape.Shape;

			StateMachine = new();
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

			StateMachine?.RunState(Data);
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
