using System;
using static Gamedev.Main.Characters.Players.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;
using static Gamedev.Main.Characters.Players.PlayerFSM;
using Godot;

namespace Gamedev.Main.Characters.Players
{
	public class WallState : PlayerState
	{
		public override State State { get; } = State.Wall;

		protected override Func<PlayerData, State>[] Transitions { get; }

		public WallState()
		{
			Transitions = new[]
			{
				FallTransition,
				WallJumpingTransition,
				GroundedTransition,
			};
		}

		public override void Execute(PlayerData data)
		{
			//data.Player.Position = new(MathF.Round(data.Player.Position.X), data.Player.Position.Y);
			data.ResetTimers();
			data.Velocity = data.Gravity * data.WallSlideModifier;
			data.Sprite.Travel(AnimationState.Wall);
		}

		private State WallJumpingTransition(PlayerData data)
		{
			if (data.JumpJustPressed)
			{
				if (data.Facing == Direction.East)
				{
					data.Particles.WallJumpParticles.Scale = new(1, 1);
				} else {
					data.Particles.WallJumpParticles.Scale = new(-1, 1);
				}
				data.Particles.WallJumpParticlesEmitting = true;
				return State.WallJumping;
			}
			return State.Invalid;
		}

		private State FallTransition(PlayerData data)
		{
			if (data.InputDirection.ToQuadrantDirection() != data.WallSide)
			{
				return State.Falling;
			}
			return State.Invalid;
		}

		private State GroundedTransition(PlayerData data)
		{
			return data.Player.IsOnFloor() ? State.Grounded : State.Invalid;
		}
	}
}