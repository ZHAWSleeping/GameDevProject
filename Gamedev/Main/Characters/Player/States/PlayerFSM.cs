using System.Collections.Generic;
using System.Linq;

namespace Gamedev.Main.Characters.Player
{
	public class PlayerFSM
	{

		public enum State
		{
			Grounded,
			Jumping,
			Falling,
			Wall,
			Invalid,
		}

		private Dictionary<State, PlayerState> States { get; }

		public PlayerFSM()
		{
			this.States = States;
			PlayerState[] states = new PlayerState[] {
				new GroundedState(),
				new JumpingState(),
				new WallState(),
				new FallingState(),
			};
			States = states.ToDictionary(state => state.State);
		}

		public void RunState(PlayerData data)
		{
			State stateResult = States[data.State].Transition(data);
			data.PreviousState = data.State;
			data.State = stateResult;
			States[data.State].Execute(data);
		}
	}
}