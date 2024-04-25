
using Godot;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.Characters.Player
{
	public record class PlayerDataOld(Player Player)
	{
		public Player Player { get; } = Player;
		public float MovementSpeed { get; set; }
		public float JumpVelocity { get; set; }
		public float JumpVelocityIncrement { get; set; }
		public Player.State State { get; set; } = Player.State.Grounded;
		public Player.State PreviousState { get; set; } = Player.State.Grounded;
		public Vector2 Velocity { get; set; } = Vector2.Zero;
		public Vector2 InputDirection { get; set; } = Vector2.Zero;
		public bool JumpHeld { get; set; } = false;
		public bool JumpJustPressed { get; set; } = false;
		public int CoyoteTime { get; set; } = 0;
		public int JumpTime { get; set; } = 0;
		public Direction WallSide = Direction.None;
		public double Delta = 0;
	}
}