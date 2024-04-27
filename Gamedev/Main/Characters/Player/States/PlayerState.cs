using System;
using Godot;
using static Gamedev.Main.Characters.Player.PlayerFSM;

namespace Gamedev.Main.Characters.Player
{
	public abstract class PlayerState
	{
		/// <summary>
		/// Which state this class represents.
		/// </summary>
		public abstract State State { get; }

		/// <summary>
		/// List of transitions that will be checked before this state is executed.
		/// </summary>
		protected abstract Func<PlayerData, State>[] Transitions { get; }

		/// <summary>
		/// Runs the logic of this state.
		/// </summary>
		/// <param name="data"></param>
		public abstract void Execute(PlayerData data);

		/// <summary>
		/// Checks relevant coditions and decides whether to transition to another state.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual State Transition(PlayerData data)
		{
			State state = State.Invalid;
			foreach (var transition in Transitions)
			{
				state = transition.Invoke(data);
				if (state != State.Invalid) return state;
			}
			return State;
		}

		/// <summary>
		/// General purpose routine to process user input into horizontal movement with drag.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="speed"></param>
		/// <param name="drag"></param>
		/// <returns></returns>
		protected virtual Vector2 Move(PlayerData data, float speed, float drag)
		{
			if (
				(
					MathF.Sign(data.Velocity.X) == MathF.Sign(data.InputDirection.X)
					&& data.Velocity.X <= data.MaxMovementSpeed
					&& data.Velocity.X >= -data.MaxMovementSpeed
				)
				|| MathF.Sign(data.Velocity.X) != MathF.Sign(data.InputDirection.X)
				&& data.InputDirection != Vector2.Zero
			)
			{
				return new(data.Velocity.X + data.InputDirection.X * speed, data.Velocity.Y);
			}
			return new(Mathf.MoveToward(data.Velocity.X, 0, drag), data.Velocity.Y);
		}

	}
}