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

			
			// Accelerate
			if (data.InputDirection == Vector2.Zero)
			{
				data.Velocity = new(Mathf.MoveToward(data.Velocity.X, 0, data.MovementSpeed * data.AirborneModifier), data.Velocity.Y);
			}
			else
			{
				data.Velocity = new(data.Velocity.X + data.InputDirection.X * data.MovementSpeed * data.AirborneModifier, data.Velocity.Y);
			}
			
			if (data.PreviousState != State.WallJumping)
			{
				data.Velocity = (Vector2.Up + data.WallSide.ToVector().Rotated(MathF.PI)) * data.WallJumpVelocity;
				LastWallSide = data.WallSide;
				data.Sprite.Travel(AnimationState.Jump);
			}
			else
			{
				data.Velocity = new(data.Velocity.X, data.Velocity.Y - data.WallJumpVelocityIncrement);
				data.Velocity += data.Gravity;
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