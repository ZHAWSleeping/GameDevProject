using System;
using Gamedev.Main.Constants;
using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Gamedev.Main.Levels;
using Gamedev.Main.Objects.Cards;
using Godot;

namespace Gamedev.Main.Characters.Players
{
	public partial class Player : CharacterBody2D
	{
		[Export]
		public PlayerData Data;

		[Export]
		private PlayerSprite Sprite;

		[Export]
		private PlayerAudioManager AudioManager;

		[Export]
		private PlayerParticleManager Particles;

		[Export]
		public int BatteryCount = 0;

		[Export]
		private CollisionShape2D Shape;

		[Export]
		private ShapeCast2D LeftWallCast;

		[Export]
		private ShapeCast2D RightWallCast;

		private PlayerFSM StateMachine;
		private CardInventory Inventory;

		public override void _Ready()
		{
			base._Ready();
			CollisionEvents.CollisionDeath += Die;
			CollisionEvents.BatteryCollected += BatteryCollected;
			CollisionEvents.LightTouched += CheckForBatteries;
			CollisionEvents.ObjectBroken += () => AudioManager.Play(PlayerAudioManager.Sound.Break);
			CollisionEvents.GoalReached += Win;

			Data.Player = this;
			Data.Sprite = Sprite;
			Data.Audio = AudioManager;
			Data.Particles = Particles;
			Data.LeftWallCast = LeftWallCast;
			Data.RightWallCast = RightWallCast;
			Data.Shape = (RectangleShape2D)Shape.Shape;

			StateMachine = new();
			Inventory = new();

		}


		public override void _PhysicsProcess(double delta)
		{
			if (Data.JustRespawned == 0)
			{
				GameStateEvents.OnPlayerRespawned(GlobalPosition);
				Data.JustRespawned--;
			}
			else if (Data.JustRespawned > 0)
			{
				Data.JustRespawned--;
			}
			Data.Velocity = Velocity;
			Data.InputDirection = new Vector2(InputExtensions.MovementVector().X, 0);
			Data.JumpHeld = Inputs.Jump.Pressed();
			Data.JumpJustPressed = Inputs.Jump.JustPressed();
			Data.DiscardJustPressed = Inputs.Discard.JustPressed();
			Data.Delta = delta;
			PollDiscard();

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

			if (Data.Velocity.ToQuadrantDirection() == VectorExtensions.Direction.East || Data.WallSide == VectorExtensions.Direction.East)
			{
				Sprite.FlipH = false;
				Data.Facing = VectorExtensions.Direction.East;
			}
			else if (Data.Velocity.ToQuadrantDirection() == VectorExtensions.Direction.West || Data.WallSide == VectorExtensions.Direction.West)
			{
				Sprite.FlipH = true;
				Data.Facing = VectorExtensions.Direction.West;
			}

			if (Data.State == PlayerFSM.State.Wall)
			{
				Data.VisuallyFacing = Sprite.FlipH
					? VectorExtensions.Direction.West
					: VectorExtensions.Direction.East;
			}
			else
			{
				Data.VisuallyFacing = Data.Facing;
			}


			Velocity = Data.Velocity;
			UpdateDebug();
			MoveAndSlide();
		}

		private void PollDiscard()
		{
			if (Data.DiscardJustPressed)
			{
				PowerUpCard card = Inventory.Consume();
				bool valid = true;
				switch (card.CardType)
				{
					case PowerUpCard.Type.DoubleJump:
						Data.State = PlayerFSM.State.DoubleJumping;
						break;
					case PowerUpCard.Type.Dash:
						Data.State = PlayerFSM.State.Dashing;
						break;
					case PowerUpCard.Type.Stomp:
						Data.State = PlayerFSM.State.Stomping;
						break;
					default:
						valid = false;
						break;
				}

				if (valid)
				{
					CollisionEvents.OnCardConsumed(card);
					PersistentEvents.OnCardConsumed(card);
					Data.ResetTimers();
				}
			}
		}

		private void Die()
		{
			GameStateEvents.OnPlayerDied(GlobalPosition);
			PersistentEvents.OnPlayerDied(GlobalPosition);
			AudioManager.Play(PlayerAudioManager.Sound.Death);
			this.SetProcessModeDeferred(ProcessModeEnum.Disabled);
			Tween tween = LevelManager.Instance.CreateTween();
			tween.TweenCallback(Callable.From(() => PersistentEvents.OnLevelChangeRequested(LevelManager.Instance.State))).SetDelay(0.1f);
		}

		private void Win()
		{
			AudioManager.Play(PlayerAudioManager.Sound.Victory);
			this.SetProcessModeDeferred(ProcessModeEnum.Disabled);
			Tween tween = LevelManager.Instance.CreateTween();
			tween.TweenCallback(Callable.From(() => PersistentEvents.OnLevelFinished(LevelManager.Instance.State))).SetDelay(1.6f);
		}

		private void BatteryCollected()
		{
			BatteryCount++;
		}

		private void CheckForBatteries()
		{
			if (BatteryCount > 0)
			{
				CollisionEvents.OnLightActivated();
				PersistentEvents.OnLightActivated();
				BatteryCount--;
			}
		}

		private void UpdateDebug()
		{
			DebugEvents.OnPlayerDataEvent(Data);
		}
	}
}
