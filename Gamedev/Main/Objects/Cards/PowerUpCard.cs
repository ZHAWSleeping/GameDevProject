using Gamedev.Main.Characters.Player;
using Godot;

namespace Gamedev.Main.Objects.Cards
{
	public class PowerUpCard
	{
		public enum Type
		{
			Invalid,
			DoubleJump,
			Dash,
			Stomp,
		}

		public PlayerFSM.State CardState { get; }
		public Type CardType { get; }

		public PowerUpCard(Type type, PlayerFSM.State state)
		{
			CardType = type;
			CardState = state;
		}
	}
}