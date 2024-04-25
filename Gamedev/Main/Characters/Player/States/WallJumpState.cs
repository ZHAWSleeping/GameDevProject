using System;
using static Gamedev.Main.Characters.Player.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;
using static Gamedev.Main.Characters.Player.PlayerFSM;
using Godot;

namespace Gamedev.Main.Characters.Player
{
	public class WallJumpState : PlayerState
	{
		public override State State { get; } = State.WallJumping;

		protected override Func<PlayerData, State>[] Transitions { get; }

		private Direction LastWallSide = Direction.None;

		public WallJumpState()
		{
			Transitions = new[]
			{
				FallTransition,
			};
		}

		public override void Execute(PlayerData data)
		{
			data.JumpTime--;

			/*
			// Accelerate
			if (data.InputDirection == Vector2.Zero)
			{
				data.Velocity = new(Mathf.MoveToward(data.Velocity.X, 0, data.MovementSpeed * data.AirborneModifier), data.Velocity.Y);
			}
			else
			{
				data.Velocity = new(data.InputDirection.X * data.MovementSpeed, data.Velocity.Y);
			}
			*/
			if (data.PreviousState == State.Wall)
			{
				data.Velocity = (Vector2.Up + data.WallSide.ToVector().Rotated(MathF.PI)) * data.MovementSpeed;
				LastWallSide = data.WallSide;
				data.Sprite.Travel(AnimationState.Jump);
			}
			else
			{
				data.Velocity = (Vector2.Up + LastWallSide.ToVector().Rotated(MathF.PI)) * data.MovementSpeed * 0.5f;
				data.Velocity += data.Player.GetGravity() * (float)data.Delta;
			}
		}

		private State FallTransition(PlayerData data)
		{
			return !data.JumpHeld || data.JumpTime <= 0
				? State.Falling
				: State.Invalid;
		}


	}
}