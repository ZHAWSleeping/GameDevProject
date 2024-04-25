using System;
using Godot;
using static Gamedev.Main.Characters.Player.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;
using static Gamedev.Main.Characters.Player.PlayerFSM;

namespace Gamedev.Main.Characters.Player
{
	public class JumpingState : PlayerState
	{
		public override State State { get; } = State.Jumping;
		protected override Func<PlayerData, State>[] Transitions { get; }

		public JumpingState()
		{
			Transitions = new[]
			{
				FallingTransition,
				WallTransition,
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
				data.Velocity = new(data.InputDirection.X * data.MovementSpeed, data.Velocity.Y);
			}

			switch (data.PreviousState)
			{
				case State.Grounded:
					data.Velocity = new(data.Velocity.X, data.JumpVelocity);
					data.Sprite.Travel(AnimationState.Jump);
					break;
				case State.Wall:
					data.Sprite.Travel(AnimationState.Jump);

					break;
				default:
					data.Velocity = new(data.Velocity.X, data.Velocity.Y - 500 * (float)data.Delta);
					data.Velocity += data.Player.GetGravity() * (float)data.Delta;
					break;
			}

		}

		private State FallingTransition(PlayerData data)
		{
			return !data.JumpHeld || data.JumpTime <= 0
				? State.Falling
				: State.Invalid;
		}

		private State WallTransition(PlayerData data)
		{
			return data.WallSide != Direction.None ? State.Wall : State.Invalid;
		}
	}
}