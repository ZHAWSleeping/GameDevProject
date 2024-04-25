using System;
using static Gamedev.Main.Characters.Player.PlayerFSM;

namespace Gamedev.Main.Characters.Player
{
	public abstract class PlayerState
	{
		/// <summary>
		/// Which state this class represents.
		/// </summary>
		public abstract State State { get; }
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
	}
}