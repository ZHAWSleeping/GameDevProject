using System.Collections.Generic;
using System.Linq;

namespace Gamedev.Main.Characters.Player
{
	/// <summary>
	/// The player's movement state machine. Transitions are checked first. Then, the state gets executed.
	/// </summary>
	public class PlayerFSM
	{

		public enum State
		{
			Grounded,
			Jumping,
			Falling,
			Wall,
			WallJumping,
			Invalid,
			DoubleJumping,
			Dashing,
			Stomping,
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
				new WallJumpState(),
			};
			States = states.ToDictionary(state => state.State);
		}

		/// <summary>
		/// Ticks the state machine.
		/// </summary>
		/// <param name="data"></param>
		public void RunState(PlayerData data)
		{
			State stateResult = States[data.State].Transition(data);
			data.PreviousState = data.State;
			data.State = stateResult;
			States[data.State].Execute(data);
		}
	}
}