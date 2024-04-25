using System;
using static Gamedev.Main.Characters.Player.Player;

namespace Gamedev.Main.Characters.Player
{
	public abstract class PlayerState
	{
		public abstract State State { get; }
		protected abstract Func<PlayerData, State>[] Transitions { get; }

		public abstract void Execute(PlayerData data);

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