using System;
using static Gamedev.Main.Characters.Player.PlayerSprite;
using static Gamedev.Main.Extensions.VectorExtensions;
using static Gamedev.Main.Characters.Player.PlayerFSM;

namespace Gamedev.Main.Characters.Player
{
	public class WallState : PlayerState
	{
		public override State State { get; } = State.Wall;

		protected override Func<PlayerData, State>[] Transitions { get; }

		public WallState()
		{
			Transitions = new[]
			{
				JumpingTransition,
				WallTransition,
			};
		}

		public override void Execute(PlayerData data)
		{
			data.Player.Position = new(MathF.Round(data.Player.Position.X), data.Player.Position.Y);
			ResetTimers(data);
			data.Velocity = data.Player.GetGravity() * 0.5f * (float)data.Delta;
			data.Sprite.Travel(AnimationState.Wall);


		}

		private State JumpingTransition(PlayerData data)
		{
			return data.JumpJustPressed ? State.Jumping : State.Invalid;
		}

		private State WallTransition(PlayerData data)
		{
			if (data.WallSide == Direction.None || data.InputDirection.ToQuadrantDirection() != data.WallSide)
			{
				return State.Falling;
			}
			return State.Invalid;
		}

		private void ResetTimers(PlayerData data)
		{
			data.JumpTime = data.JumpTimeFrames;
			data.CoyoteTime = data.CoyoteTimeFrames;
		}

	}
}