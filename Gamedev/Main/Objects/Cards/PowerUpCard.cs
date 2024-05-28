using Gamedev.Main.Characters.Players;
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
		public Color CardColor { get; }

		public PowerUpCard(Type type, PlayerFSM.State state, Color color)
		{
			CardType = type;
			CardState = state;
			CardColor = color;
		}
	}
}